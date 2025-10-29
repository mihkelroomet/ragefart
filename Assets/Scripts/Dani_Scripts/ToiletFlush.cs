using UnityEngine;

public class ToiletFlush : MonoBehaviour
{
    [SerializeField] private ParticleSystem flushEffect;
    [SerializeField] private Animator flushToilet;

    public void TriggerFlushEffect()
    {
        flushEffect.Emit(Random.Range(10, 20));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            flushToilet.Play("ToiletFlush", -1, 0);

        }
    }
}
