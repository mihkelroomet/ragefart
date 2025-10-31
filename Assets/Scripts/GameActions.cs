using UnityEngine;

[CreateAssetMenu(menuName="Game/Game Actions")]
public class GameActions : ScriptableObject
{
    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBGL
        WebGL_Quit();
    #else
        Application.Quit();
    #endif
    }
    
    public void WebGL_Quit()
    {
        SceneFader.I.LoadScene("StartHere");
    }
}
