using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio  Source ----------")]
    public AudioSource sfxSource;
    public AudioSource MusicSource;

    [Header("---------- Audio  Clip ----------")]
    public AudioClip shootClip;
    public AudioClip enemyDeathClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
