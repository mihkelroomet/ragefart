using UnityEngine;

public class IGCanvas : MonoBehaviour
{
    [SerializeField] WinEvent winEvent;
    [SerializeField] LoseEvent loseEvent;
    
    [SerializeField] RoundEndPanel roundEndPanel;
    [SerializeField] RoundResultsPanel roundResultsPanel;

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
        roundEndPanel.OnWin();
        roundResultsPanel.gameObject.SetActive(false);
        roundEndPanel.gameObject.SetActive(true);
    }

    void OnLose(Events.Lose _)
    {
        roundEndPanel.OnLose();
        roundResultsPanel.gameObject.SetActive(false);
        roundEndPanel.gameObject.SetActive(true);
    }
}
