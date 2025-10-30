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
        print("updating");
        // Removing old round result images
        foreach (Transform child in gameObject.transform)
        {
            print("removing");
            Destroy(child.gameObject);
        }
        
        // Adding new ones
        foreach (bool roundResult in gameState.roundResults)
        {
            if (roundResult)
            {
                print("pos result");
                Instantiate(roundResultImageWon, gameObject.transform);
            }
            else
            {
                print("neg result");
                Instantiate(roundResultImageLost, gameObject.transform);
            }
        }
        
        print("current");
        Instantiate(roundResultImageCurrent, gameObject.transform);

        for (int i = gameState.roundResults.Count + 1; i < gameState.TotalRounds; i++)
        {
            print("future");
            Instantiate(roundResultImageFuture, gameObject.transform);
        }
    }
}
