using UnityEngine;

public class Toilet : MonoBehaviour
{
    [SerializeField] PlaySFXEvent playSFXEvent;
    [SerializeField] Sound splashSound;
    
    [SerializeField] WinEvent winEvent;
    
    [SerializeField] GameState gameState;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // ReSharper disable once InvertIf
        if (gameState.gameIsRunning && other.CompareTag("Player"))
        {
            playSFXEvent.Raise(new Events.PlaySFX(splashSound));
            winEvent.Raise(new Events.Win());
        }
    }
}
