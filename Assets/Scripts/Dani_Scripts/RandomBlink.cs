using UnityEngine;
using System.Collections;

public class RandomBlink : MonoBehaviour
{
    [SerializeField] private Animator characterSpriteAnimator;
    [SerializeField] private Vector2 blinkInterval;

    void Start()
    {
        StartCoroutine(CharacterBlink());
    }

    IEnumerator CharacterBlink()
    {
        yield return new WaitForSeconds(Random.Range(blinkInterval.x,blinkInterval.y));

        characterSpriteAnimator.Play("CharacterBlink", -1,0);
        StartCoroutine(CharacterBlink());
    }

    

   
}
