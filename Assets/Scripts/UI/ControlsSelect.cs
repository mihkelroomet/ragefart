using UnityEngine;

public class ControlsSelect : MonoBehaviour
{
    public void SwitchActionMap(string actionMap)
    {
        GetComponent<UnityEngine.InputSystem.PlayerInput>().SwitchCurrentActionMap(actionMap);
    }
}
