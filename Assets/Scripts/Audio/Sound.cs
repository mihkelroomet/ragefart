using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName="Audio/Sound")]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    public AudioMixerGroup output;

    public void ApplyTo(AudioSource src)
    {
        src.clip = clip;
        src.outputAudioMixerGroup = output;
        src.spatialBlend = 0f; // 0 = 2D
    }
}
