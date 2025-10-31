using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] GameState gameState;
    
    [SerializeField] Sprite victoryImage;
    [SerializeField] Sprite defeatImage;
    
    [SerializeField] Image gameResultTitleImage;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI instructionsText;
    [SerializeField] TextMeshProUGUI hintText;

    void Start()
    {
        UpdateTitleImage();
        UpdateFinalScoreText();
        UpdateInstructionsText();
        UpdateHintText();
    }
    
    void UpdateTitleImage()
    {
        gameResultTitleImage.sprite = gameState.GameIsBeat() ? victoryImage : defeatImage;
    }

    void UpdateFinalScoreText()
    {
        finalScoreText.text = "Final Score: " + gameState.CountWins() + "/" + gameState.TotalRounds;
    }

    void UpdateInstructionsText()
    {
        if (gameState.GameIsBeat())
        {
            instructionsText.text = "Grats, u r a fart master!";
        }
        else
        {
            instructionsText.text = "Score " + gameState.minWinsToBeatGame + "+/" + gameState.TotalRounds + " to beat game";
        }
    }

    void UpdateHintText()
    {
        if (gameState.GameIsBeat())
        {
            hintText.text = "";
        }
        else
        {
            int hintNr = gameState.HintPool.Length > gameState.iteration ? gameState.iteration : gameState.HintPool.Length - 1;
            hintText.text = "Hint: " + gameState.HintPool[hintNr];
        }
    }
}
