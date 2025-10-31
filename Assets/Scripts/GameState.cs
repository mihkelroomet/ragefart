using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/Game State")]
public class GameState : ScriptableObject
{
    public bool gameIsRunning;
    public List<bool> roundResults;
    public int minWinsToBeatGame = 6;
    [SerializeField] int totalRounds = 10;
    public int TotalRounds => totalRounds;
    public int iteration;
    [SerializeField] string[] stagePool;
    [SerializeField] string[] hintPool;
    public string[] HintPool => hintPool;
    public string actionMap;

    public void LoadControlsSelect()
    {
        SceneFader.I.LoadScene("ControlsSelect");
    }

    string GetRandomStage()
    {
        return stagePool[Random.Range(0, stagePool.Length)];
    }

    public void LoadRandomStage()
    {
        SceneFader.I.LoadScene(GetRandomStage());
    }

    public void SwitchActionMap(string map)
    {
        actionMap = map;
    }

    public void Restart()
    {
        iteration = GameIsBeat() ? 0 : iteration + 1;
        gameIsRunning = false;
        roundResults.Clear();
        LoadRandomStage();
    }

    public int CountWins()
    {
        return roundResults.Count(x => x);
    }

    public bool GameIsBeat()
    {
        return CountWins() >= minWinsToBeatGame;
    }

    public static void LoadEndScreen()
    {
        SceneFader.I.LoadScene("EndScreen");
    }
}
