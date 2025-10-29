using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game State")]
public class GameState : ScriptableObject
{
    public bool gameIsRunning;
    public bool[] roundResults;
    public int iteration;
}
