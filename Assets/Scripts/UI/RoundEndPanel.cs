using UnityEngine;
using UnityEngine.UI;

public class RoundEndPanel : MonoBehaviour
{
    [SerializeField] WinEvent winEvent;
    [SerializeField] LoseEvent loseEvent;
    
    [SerializeField] Sprite winImage;
    [SerializeField] Sprite loseImage;

    [SerializeField] Image RoundResultTitleImage;
    
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
        RoundResultTitleImage.sprite = winImage;
    }

    void OnLose(Events.Lose _)
    {
        RoundResultTitleImage.sprite = loseImage;
    }
}
