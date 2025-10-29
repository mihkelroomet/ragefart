using UnityEngine;

public class IGCanvas : MonoBehaviour
{
    [SerializeField] WinEvent winEvent;
    [SerializeField] RectTransform winPanel;

    void OnEnable()
    {
        winEvent.OnEvent += OnWin;
    }

    void OnDisable()
    {
        winEvent.OnEvent -= OnWin;
    }

    void OnWin(Events.Win _)
    {
        winPanel.gameObject.SetActive(true);
    }
}
