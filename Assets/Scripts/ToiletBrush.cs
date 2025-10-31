using System.Collections;
using UnityEngine;

public class ToiletBrush : MonoBehaviour
{
    [SerializeField] PlaySFXEvent playSFXEvent;
    [SerializeField] Sound splatSound;
    
    [SerializeField] float launchDelay;
    [SerializeField] float accel = 60f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] Transform target;

    Vector2 _initialLocation;
    Vector2 _moveDirection;
    Vector2 _targetLocation;
    bool _isMoving;
    
    [SerializeField] LoseEvent loseEvent;
    
    [SerializeField] GameState gameState;
    
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialLocation = transform.position;
        _targetLocation = target.transform.position;
        _moveDirection = (_targetLocation - _initialLocation).normalized;
    }

    void FixedUpdate()
    {
        if (!_isMoving) return;

        // if past target
        if (((Vector2)transform.position - _initialLocation).magnitude >= (_targetLocation - _initialLocation).magnitude)
        {
            _isMoving = false;
            return;
        }
        
        SetSpeed();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ReSharper disable once InvertIf
        if (gameState.gameIsRunning && other.CompareTag("Player"))
        {
            playSFXEvent.Raise(new Events.PlaySFX(splatSound));
            Destroy(other.gameObject);
            loseEvent.Raise(new Events.Lose());
        }
    }

    void SetSpeed()
    {
        _rb.linearVelocity = Vector2.MoveTowards(_rb.linearVelocity, _moveDirection * maxSpeed, accel * Time.fixedDeltaTime);
    }

    public void Launch()
    {
        StartCoroutine(LaunchCoroutine());
    }

    IEnumerator LaunchCoroutine()
    {
        yield return new WaitForSeconds(launchDelay);
        _isMoving = true;
    }
}
