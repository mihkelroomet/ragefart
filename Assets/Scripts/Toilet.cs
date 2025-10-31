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
    [SerializeField] float animStartToSplashDelay = 1.0f;
    [SerializeField] float splashToWinDelay = 0.5f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // ReSharper disable once InvertIf
        if (gameState.gameIsRunning && other.CompareTag("Player"))
        {
            playerObject.SetActive(false);
            winEvent.Raise(new Events.Win());
            StartCoroutine(GameWinInitiated());
        }
    }

    IEnumerator GameWinInitiated()
    {
        toiletAnimator.Play("ToiletFlush");
        yield return new WaitForSeconds(animStartToSplashDelay);
        playSFXEvent.Raise(new Events.PlaySFX(splashSound));
        yield return new WaitForSeconds(splashToWinDelay);
    }
    
}
