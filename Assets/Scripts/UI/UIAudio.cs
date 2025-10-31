using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public static UIAudio I { get; private set; }
    [SerializeField] PlaySFXEvent playSFXEvent;
    [SerializeField] Sound chooseSound;
    [SerializeField] Sound selectSound;
    
    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Hover() { if (chooseSound) playSFXEvent.Raise(new Events.PlaySFX(chooseSound)); }
    public void Click() { if (selectSound) playSFXEvent.Raise(new Events.PlaySFX(selectSound)); }
}