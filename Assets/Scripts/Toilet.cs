using UnityEngine;
using System.Collections;
public class Toilet : MonoBehaviour
{
    [SerializeField] PlaySFXEvent playSFXEvent;
    [SerializeField] Sound splashSound;
    
    [SerializeField] WinEvent winEvent;
    
    [SerializeField] GameState gameState;
    [SerializeField] Animator toiletAnimator;
    [SerializeField] GameObject playerObject;
    [SerializeField] private float delay;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // ReSharper disable once InvertIf
        if (gameState.gameIsRunning && other.CompareTag("Player"))
        {
            playSFXEvent.Raise(new Events.PlaySFX(splashSound));
            StartCoroutine(GameWinInitiated());
        }
    }

    IEnumerator GameWinInitiated()
    {
        playerObject.SetActive(false);
        toiletAnimator.Play("ToiletFlush");
        yield return new WaitForSeconds(delay);
        winEvent.Raise(new Events.Win());
    }
    
}
