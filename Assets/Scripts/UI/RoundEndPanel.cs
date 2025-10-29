using UnityEngine;
using UnityEngine.UI;

public class RoundEndPanel : MonoBehaviour
{
    [SerializeField] Sprite winImage;
    [SerializeField] Sprite loseImage;

    [SerializeField] Image roundResultTitleImage;

    public void OnWin()
    {
        roundResultTitleImage.sprite = winImage;
    }

    public void OnLose()
    {
        roundResultTitleImage.sprite = loseImage;
    }
}
