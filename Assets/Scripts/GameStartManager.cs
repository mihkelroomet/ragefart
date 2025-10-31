using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] GameState gameState;

    void Start()
    {
        gameState.gameIsRunning = false;
        gameState.roundResults.Clear();
        gameState.iteration = 0;
    }
}
