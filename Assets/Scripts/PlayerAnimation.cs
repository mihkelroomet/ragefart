using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    [SerializeField] Animator animator;
    PlayerMovement _playerMovement;
    
    InputAction _fartAction;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();

        _fartAction = InputSystem.actions.FindAction("Player/Fart");
    }
    void Update()
    {
        bool isFarting = _fartAction.IsPressed();
        SetAnimation(_playerMovement.MoveInput, isFarting);
    }

    void SetAnimation(float moveInput, bool isFarting)
    {
        if (moveInput != 0)
        {
            _sr.flipX = moveInput < 0;
        }
        
        string animationName;

        if (isFarting)
        {
            animationName = "Player_Fart";
        }
        else if (_playerMovement.IsOnGround)
        {
            animationName = moveInput == 0 ? "Player_Idle" : "Player_Run";
        }
        else
        {
            animationName = _rb.linearVelocityY > 0 ? "Player_Jump" : "Player_Fall";
        }
        
        animator.Play(animationName);
    }
}
