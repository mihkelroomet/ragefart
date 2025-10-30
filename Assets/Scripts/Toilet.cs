using UnityEngine;

public class Toilet : MonoBehaviour
{
    [SerializeField] WinEvent winEvent;
    
    [SerializeField] GameState gameState;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameState.gameIsRunning && other.CompareTag("Player"))
        {
            winEvent.Raise(new Events.Win());
        }
    }
}
