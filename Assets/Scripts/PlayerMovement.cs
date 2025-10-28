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
    public bool IsOnGround { get; private set; } = true;


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

    public float MoveInput { get; private set; }

    MovingPlatform _currentMovingPlatform;

    Rigidbody2D _rb;
    Collider2D _collider;
    
    ContactFilter2D _groundContactFilter;
    [SerializeField, Range(0, 89)] float maxGroundSlopeAngle = 80f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        
        ConfigGroundContactFilter();

        _moveAction = InputSystem.actions.FindAction("Player/MoveX");
        _jumpAction = InputSystem.actions.FindAction("Player/Jump");
    }

    void Update()
    {
        MoveInput = _moveAction.ReadValue<float>();

        if (JumpBuffered) return;
        JumpBuffered = _jumpAction.WasPressedThisFrame();
        _jumpBufferTimer = StartCoroutine(JumpBufferTimer());
    }

    void FixedUpdate()
    {
        IsOnGround = _collider.IsTouching(_groundContactFilter);

        SetHorizontalSpeed();
        
        // If on ground and either on it or at the end of falling onto it
        // Small value for comparison because for some reason velY is not zero when moving horizontally
        if (IsOnGround && _rb.linearVelocityY <= 0.001)
        {
            _canJump = true;
        }

        if (_currentMovingPlatform)
        {
            _rb.linearVelocity += _currentMovingPlatform.Velocity;
        }

        bool walkedOffGround = _wasOnGround && !IsOnGround && _rb.linearVelocityY <= 0;
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

        _wasOnGround = IsOnGround;
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
        float targetSpeed = MoveInput * maxSpeed;
        float accel = IsOnGround ? groundAccel : airAccel;
        float friction = IsOnGround ? groundFriction : airFriction;

        float newVelX;
        // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
        if (Mathf.Abs(MoveInput) > 0.01f) // if speeding up
        {
            newVelX = Mathf.MoveTowards(currentVelX, targetSpeed, accel * Time.fixedDeltaTime);
        }
        else // if slowing down
        {
            newVelX = Mathf.MoveTowards(currentVelX, 0, friction * Time.fixedDeltaTime);
        }
        
        _rb.linearVelocityX = newVelX;
        print("target speed: " + targetSpeed);
        print("on ground: " + IsOnGround);
        print("move input: " + MoveInput);
        print("hor vel set to " + newVelX);
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
}
