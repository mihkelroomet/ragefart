using UnityEngine;

public class RoundResultsPanel : MonoBehaviour
{
    [SerializeField] GameState gameState;
    
    [SerializeField] Transform roundResultImageFuture;
    [SerializeField] Transform roundResultImageCurrent;
    [SerializeField] Transform roundResultImageWon;
    [SerializeField] Transform roundResultImageLost;

    void Start()
    {
        UpdateRoundResultImages();
    }
    
    void UpdateRoundResultImages()
    {
        // Removing old round result images
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        
        // Adding new ones
        foreach (bool roundResult in gameState.roundResults)
        {
            if (roundResult)
            {
                Instantiate(roundResultImageWon, gameObject.transform);
            }
            else
            {
                Instantiate(roundResultImageLost, gameObject.transform);
            }
        }
        
        Instantiate(roundResultImageCurrent, gameObject.transform);

        for (int i = gameState.roundResults.Count + 1; i < gameState.TotalRounds; i++)
        {
            Instantiate(roundResultImageFuture, gameObject.transform);
        }
    }
}
