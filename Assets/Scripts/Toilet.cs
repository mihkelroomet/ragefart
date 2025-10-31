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
    [SerializeField] float animStartToSplashDelay = 0.8f;
    [SerializeField] float splashToWinDelay = 0.7f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // ReSharper disable once InvertIf
        if (gameState.gameIsRunning && other.CompareTag("Player"))
        {
            StartCoroutine(GameWinInitiated());
        }
    }

    IEnumerator GameWinInitiated()
    {
        playerObject.SetActive(false);
        toiletAnimator.Play("ToiletFlush");
        yield return new WaitForSeconds(animStartToSplashDelay);
        playSFXEvent.Raise(new Events.PlaySFX(splashSound));
        yield return new WaitForSeconds(splashToWinDelay);
        winEvent.Raise(new Events.Win());
    }
    
}
