using UnityEngine;

public class Toilet : MonoBehaviour
{
    [SerializeField] WinEvent winEvent;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winEvent.Raise(new Events.Win());
        }
    }
}
