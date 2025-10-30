using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/Game State")]
public class GameState : ScriptableObject
{
    public bool gameIsRunning;
    public bool[] roundResults;
    public int iteration;
    [SerializeField] string[] stagePool;
    public string[] StagePool => stagePool;

    string GetRandomStage()
    {
        return stagePool[Random.Range(0, stagePool.Length)];
    }

    public void LoadRandomStage()
    {
        SceneManager.LoadScene(GetRandomStage());
    }
}
