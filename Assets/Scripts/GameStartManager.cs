using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] GameState gameState;

    void Start()
    {
        gameState.LoadRandomStage();
    }
}
