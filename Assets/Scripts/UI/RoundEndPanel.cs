using UnityEngine;
using UnityEngine.UI;

public class RoundEndPanel : MonoBehaviour
{
    [SerializeField] GameState gameState;
    
    [SerializeField] Sprite winImage;
    [SerializeField] Sprite loseImage;

    [SerializeField] Image roundResultTitleImage;
    [SerializeField] Transform roundResultImages;

    [SerializeField] Transform roundResultImageFuture;
    [SerializeField] Transform roundResultImageWon;
    [SerializeField] Transform roundResultImageLost;

    [SerializeField] Button advanceButton;

    public void OnShowRoundEndPanel()
    {
        UpdateTitleImage();
        UpdateRoundResultImages();
        UpdateAdvanceButtonFunc();
    }

    void UpdateTitleImage()
    {
        roundResultTitleImage.sprite = gameState.roundResults[^1] ? winImage : loseImage;
    }

    void UpdateRoundResultImages()
    {
        // Removing old round result images
        foreach (Transform child in roundResultImages)
        {
            Destroy(child.gameObject);
        }
        
        // Adding new ones
        foreach (bool roundResult in gameState.roundResults)
        {
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (roundResult)
            {
                Instantiate(roundResultImageWon, roundResultImages);
            }
            else
            {
                Instantiate(roundResultImageLost, roundResultImages);
            }
        }

        for (int i = gameState.roundResults.Count; i < gameState.TotalRounds; i++)
        {
            Instantiate(roundResultImageFuture, roundResultImages);
        }
    }

    void UpdateAdvanceButtonFunc()
    {
        if (gameState.roundResults.Count >= gameState.TotalRounds)
        {
            advanceButton.onClick = new Button.ButtonClickedEvent();
            advanceButton.onClick.AddListener(GameState.LoadEndScreen);
        }
    }
}
