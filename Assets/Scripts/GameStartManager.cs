using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] GameState gameState;

    void Start()
    {
        SceneManager.LoadScene(gameState.GetRandomStage());
    }
}
