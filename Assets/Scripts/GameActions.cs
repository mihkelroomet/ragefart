using UnityEngine;

[CreateAssetMenu(menuName="Game/Game Actions")]
public class GameActions : ScriptableObject
{
    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBGL
        JS_Quit();
    #else
        Application.Quit();
    #endif
    }
}
