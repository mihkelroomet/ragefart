using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] PlaySFXEvent playSFXEvent;
    [SerializeField] Sound[] fartPool;
    
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
        if (_fartAction.WasPressedThisFrame()) PlayFartSound();
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

    void PlayFartSound()
    {
        // getting random sound from pool
        Sound fartSound = fartPool[Random.Range(0, fartPool.Length)];
        
        playSFXEvent.Raise(new Events.PlaySFX(fartSound));
    }
}
