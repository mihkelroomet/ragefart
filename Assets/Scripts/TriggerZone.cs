using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] GameState gameState;

    [SerializeField] ToiletBrush[] brushesToLaunch;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // ReSharper disable once InvertIf
        if (gameState.gameIsRunning && other.CompareTag("Player"))
        {
            foreach (ToiletBrush brush in brushesToLaunch)
            {
                brush.Launch();
            }
        }
    }
}
