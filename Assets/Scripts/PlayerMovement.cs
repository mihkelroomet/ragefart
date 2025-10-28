using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    public LayerMask groundLayerMask;

    public int maxJumps = 2;
    int _jumpsAvailable;

    bool _wasOnGround = true;
    bool _isOnGround = true;

    InputAction _moveAction;
    InputAction _jumpAction;

    float _moveInput;
    bool _jumpInput;
    
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
        
        _jumpsAvailable = maxJumps;
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>().x;

        if (!_jumpInput)
        {
            _jumpInput = _jumpAction.WasPressedThisFrame();
        }
        
        SetAnimation(_moveInput);
    }

    void FixedUpdate()
    {
        _isOnGround = _collider.IsTouching(_groundContactFilter);
        _rb.linearVelocityX = _moveInput * moveSpeed;
        
        // If on ground and either on it or at the end of falling onto it
        // Small value for comparison because for some reason velY is not zero when moving horizontally
        if (_isOnGround && _rb.linearVelocityY <= 0.001)
        {
            _jumpsAvailable = maxJumps;
        }

        if (_currentMovingPlatform)
        {
            _rb.linearVelocity += _currentMovingPlatform.Velocity;
        }

        bool walkedOffGround = _wasOnGround && !_isOnGround && _rb.linearVelocityY <= 0;
        if (walkedOffGround)
        {
            _jumpsAvailable--;
        }

        if (_jumpInput)
        {
            if (_jumpsAvailable > 0)
            {
                Jump();
            }
            _jumpInput = false;
        }

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

    void Jump()
    {
        _rb.linearVelocityY = jumpForce;
        _jumpsAvailable--;
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
