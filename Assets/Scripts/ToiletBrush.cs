using UnityEngine;

public class ToiletBrush : MonoBehaviour
{
    [SerializeField] LoseEvent loseEvent;
    
    [SerializeField] GameState gameState;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameState.gameIsRunning && other.CompareTag("Player"))
        {
            loseEvent.Raise(new Events.Lose());
        }
    }
}
