using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MovingPlatform : MonoBehaviour
{
    public int speed = 1; // 1 means 1/2 pixels per physics update
    private float _pixelAdjustedSpeed;
    
    public Vector2 Velocity { get;  private set; }

    [SerializeField] Transform[] moveTargets;
    
    private int _currentTargetIndex = 0;
    
    Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        transform.position = moveTargets[_currentTargetIndex].position;

        // How much you move every physics update if speed = 1
        float movementPerPhysicsUpdate = Time.fixedDeltaTime;
        // How much you have to move to move one pixel
        float movementPerPixel = Time.fixedDeltaTime;
        if (Camera.main != null && Camera.main.GetComponent<PixelPerfectCamera>() != null)
        {
            movementPerPixel = 1f / Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU;
        }
        else
        {
            Debug.LogWarning("No Pixel Perfect Camera");
        }
        _pixelAdjustedSpeed = speed / movementPerPhysicsUpdate * movementPerPixel / 2f;
    }

    void FixedUpdate()
    {
        UpdateCurrentTarget();
        Move();
    }

    void UpdateCurrentTarget()
    {
        if (Vector2.Distance(transform.position, moveTargets[_currentTargetIndex].position) < 0.01f)
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % moveTargets.Length;
        }
    }

    void Move()
    {
        Vector2 newPos = Vector2.MoveTowards(
            transform.position,
            moveTargets[_currentTargetIndex].position,
            _pixelAdjustedSpeed * Time.fixedDeltaTime
        );
        Velocity = new Vector2(newPos.x - transform.position.x, newPos.y - transform.position.y) / Time.fixedDeltaTime;
        _rb.MovePosition(newPos);
    }
}