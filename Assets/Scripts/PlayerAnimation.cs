using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    Animator _animator;
    PlayerMovement _playerMovement;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        SetAnimation(_playerMovement.MoveInput);
    }

    void SetAnimation(float moveInput)
    {
        if (moveInput != 0)
        {
            _sr.flipX = moveInput < 0;
        }
        
        string animationName;
        
        if (_playerMovement.IsOnGround)
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
