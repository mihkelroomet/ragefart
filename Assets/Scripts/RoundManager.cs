using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] WinEvent winEvent;
    [SerializeField] LoseEvent loseEvent;
    
    [SerializeField] GameState gameState;

    void Awake()
    {
        gameState.gameIsRunning = true;
    }

    void OnEnable()
    {
        winEvent.OnEvent += OnWin;
        loseEvent.OnEvent += OnLose;
    }

    void OnDisable()
    {
        winEvent.OnEvent -= OnWin;
        loseEvent.OnEvent -= OnLose;
    }

    void OnWin(Events.Win _)
    {
        gameState.gameIsRunning = false;
    }

    void OnLose(Events.Lose _)
    {
        gameState.gameIsRunning = false;
    }
}
