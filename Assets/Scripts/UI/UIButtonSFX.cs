using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class UIButtonSFX : MonoBehaviour,
    IPointerEnterHandler, IPointerClickHandler, ISelectHandler
{
    Selectable _sel;

    void Awake() => _sel = GetComponent<Selectable>();

    public void OnPointerEnter(PointerEventData e)
    {
        if (_sel.interactable) UIAudio.I?.Hover();
    }

    // Keyboard/gamepad focus
    public void OnSelect(BaseEventData e)
    {
        if (_sel.interactable) UIAudio.I?.Hover();
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (_sel.interactable) UIAudio.I?.Click();
    }
}