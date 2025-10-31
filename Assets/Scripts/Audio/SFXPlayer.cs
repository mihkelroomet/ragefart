using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] PlaySFXEvent playSFXEvent;
    [SerializeField] int poolSize = 12;

    AudioSource[] _pool;
    int _next;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SFXPlayer");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
        _pool = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
            _pool[i] = gameObject.AddComponent<AudioSource>();
    }

    void OnEnable()  => playSFXEvent.OnEvent += PlaySFX;
    void OnDisable() => playSFXEvent.OnEvent -= PlaySFX;

    void PlaySFX(Events.PlaySFX e)
    {
        Sound s = e.Sound;
        AudioSource src = _pool[_next = (_next+1)%_pool.Length];
        s.ApplyTo(src);
        src.PlayOneShot(s.clip);
    }
}