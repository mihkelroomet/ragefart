using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] WinEvent winEvent;
    [SerializeField] LoseEvent loseEvent;
    [SerializeField] ShowRoundEndPanelEvent showRoundEndPanelEvent;
    
    [SerializeField] GameState gameState;

    [SerializeField] float showRoundEndPanelDelayOnWin = 2.3f;
    [SerializeField] float showRoundEndPanelDelayOnLoss = 0.8f;

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
        StartCoroutine(RaiseShowRoundEndPanelCoroutine(showRoundEndPanelDelayOnWin));
    }

    void OnLose(Events.Lose _)
    {
        gameState.gameIsRunning = false;
        gameState.roundResults.Add(false);
        StartCoroutine(RaiseShowRoundEndPanelCoroutine(showRoundEndPanelDelayOnLoss));
    }

    IEnumerator RaiseShowRoundEndPanelCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        showRoundEndPanelEvent.Raise(new Events.ShowRoundEndPanel());
    }
}
