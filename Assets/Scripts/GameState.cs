using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/Game State")]
public class GameState : ScriptableObject
{
    public bool gameIsRunning;
    public List<bool> roundResults;
    [SerializeField] int totalRounds = 10;
    public int TotalRounds => totalRounds;
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
