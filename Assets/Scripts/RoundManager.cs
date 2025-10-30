using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] WinEvent winEvent;
    [SerializeField] LoseEvent loseEvent;
    [SerializeField] ShowRoundEndPanelEvent showRoundEndPanelEvent;
    
    [SerializeField] GameState gameState;

    [SerializeField] float showRoundEndPanelDelay = 0.8f;

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
        gameState.roundResults.Add(true);
        StartCoroutine(RaiseShowRoundEndPanelCoroutine());
    }

    void OnLose(Events.Lose _)
    {
        gameState.gameIsRunning = false;
        gameState.roundResults.Add(false);
        StartCoroutine(RaiseShowRoundEndPanelCoroutine());
    }

    IEnumerator RaiseShowRoundEndPanelCoroutine()
    {
        yield return new WaitForSeconds(showRoundEndPanelDelay);
        showRoundEndPanelEvent.Raise(new Events.ShowRoundEndPanel());
    }
}
