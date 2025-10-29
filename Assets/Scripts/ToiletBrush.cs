using UnityEngine;

public class ToiletBrush : MonoBehaviour
{
    [SerializeField] LoseEvent loseEvent;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loseEvent.Raise(new Events.Lose());
        }
    }
}
