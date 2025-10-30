using UnityEngine;

public class IGCanvas : MonoBehaviour
{
    [SerializeField] ShowRoundEndPanelEvent showRoundEndPanelEvent;
    
    [SerializeField] RoundEndPanel roundEndPanel;
    [SerializeField] RoundResultsPanel roundResultsPanel;

    void OnEnable()
    {
        showRoundEndPanelEvent.OnEvent += OnShowRoundEndPanel;
    }

    void OnDisable()
    {
        showRoundEndPanelEvent.OnEvent -= OnShowRoundEndPanel;
    }

    void OnShowRoundEndPanel(Events.ShowRoundEndPanel _)
    {
        roundEndPanel.OnShowRoundEndPanel();
        roundResultsPanel.gameObject.SetActive(false);
        roundEndPanel.gameObject.SetActive(true);
    }
}
