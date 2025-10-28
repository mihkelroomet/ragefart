using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 9f;
    [SerializeField] float groundAccel = 60f;
    [SerializeField] float airAccel = 30f;
    [SerializeField] float groundFriction = 40f;
    [SerializeField] float airFriction = 10f;
    
    [SerializeField] float jumpForce = 18.5f;
    [SerializeField] float gravityScale = 5f;
    [SerializeField] float fallGravityScale = 8f;
    
    [SerializeField] LayerMask groundLayerMask;

    bool _canJump;

    bool _wasOnGround = true;
    bool _isOnGround = true;

    InputAction _moveAction;
    InputAction _jumpAction;

    bool _jumpBuffered;
    bool JumpBuffered
    {
        get => _jumpBuffered;
        set
        {
            if (!value && _jumpBufferTimer != null)
            {
                StopCoroutine(_jumpBufferTimer);
            }
            _jumpBuffered = value;
        }
    }
    [SerializeField] float jumpBufferTime = 0.1f;
    Coroutine _jumpBufferTimer;

    float _moveInput;
    
    MovingPlatform _currentMovingPlatform;

    SpriteRenderer _sr;
    Rigidbody2D _rb;
    Collider2D _collider;
    Animator _animator;
    
    ContactFilter2D _groundContactFilter;
    [SerializeField, Range(0, 89)] float maxGroundSlopeAngle = 80f;
    
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        
        ConfigGroundContactFilter();

        _moveAction = InputSystem.actions.FindAction("Player/Move");
        _jumpAction = InputSystem.actions.FindAction("Player/Jump");
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>().x;

        if (!JumpBuffered)
        {
            JumpBuffered = _jumpAction.WasPressedThisFrame();
            _jumpBufferTimer = StartCoroutine(JumpBufferTimer());
        }
        
        SetAnimation(_moveInput);
    }

    void FixedUpdate()
    {
        _isOnGround = _collider.IsTouching(_groundContactFilter);

        SetHorizontalSpeed();
        
        // If on ground and either on it or at the end of falling onto it
        // Small value for comparison because for some reason velY is not zero when moving horizontally
        if (_isOnGround && _rb.linearVelocityY <= 0.001)
        {
            _canJump = true;
        }

        if (_currentMovingPlatform)
        {
            _rb.linearVelocity += _currentMovingPlatform.Velocity;
        }

        bool walkedOffGround = _wasOnGround && !_isOnGround && _rb.linearVelocityY <= 0;
        if (walkedOffGround)
        {
            _canJump = false;
        }

        if (JumpBuffered)
        {
            if (_canJump)
            {
                Jump();
                JumpBuffered = false;
            }
        }

        _rb.gravityScale = _rb.linearVelocityY < 0 ? fallGravityScale : gravityScale;

        _wasOnGround = _isOnGround;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MovingPlatform"))
        {
            _currentMovingPlatform = collision.collider.GetComponent<MovingPlatform>();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MovingPlatform"))
        {
            _currentMovingPlatform = null;
        }
    }

    void ConfigGroundContactFilter()
    {
        _groundContactFilter.useLayerMask = true;
        _groundContactFilter.layerMask = groundLayerMask;
        _groundContactFilter.useNormalAngle = true;
        _groundContactFilter.minNormalAngle = 90f - maxGroundSlopeAngle; // 0°=right, 90°=up
        _groundContactFilter.maxNormalAngle = 90f + maxGroundSlopeAngle;
    }

    void SetHorizontalSpeed()
    {
        float currentVelX = _rb.linearVelocityX;
        float targetSpeed = _moveInput * maxSpeed;
        float accel = _isOnGround ? groundAccel : airAccel;
        float friction = _isOnGround ? groundFriction : airFriction;

        float newVelX;
        // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
        if (Mathf.Abs(_moveInput) > 0.01f) // if speeding up
        {
            newVelX = Mathf.MoveTowards(currentVelX, targetSpeed, accel * Time.fixedDeltaTime);
        }
        else // if slowing down
        {
            newVelX = Mathf.MoveTowards(currentVelX, 0, friction * Time.fixedDeltaTime);
        }
        
        _rb.linearVelocityX = newVelX;
    }

    void Jump()
    {
        _rb.linearVelocityY = jumpForce;
        _canJump = false;
    }

    IEnumerator JumpBufferTimer()
    {
        yield return new WaitForSeconds(jumpBufferTime);
        _jumpBuffered = false;
    }

    void SetAnimation(float moveInput)
    {
        if (moveInput != 0)
        {
            _sr.flipX = moveInput < 0;
        }
        
        string animationName;
            
        if (_isOnGround)
        {
            animationName = moveInput == 0 ? "Player_Idle" : "Player_Run";
        }
        else
        {
            animationName = _rb.linearVelocityY > 0 ? "Player_Jump" : "Player_Fall";
        }
        
        _animator.Play(animationName);
    }
}
