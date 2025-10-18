using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio  Source ----------")]
    public AudioSource sfxSource;
    public AudioSource MusicSource;

    [Header("---------- Audio  Clip ----------")]
    public AudioClip shootClip;
    public AudioClip enemyDeathClip;

    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
