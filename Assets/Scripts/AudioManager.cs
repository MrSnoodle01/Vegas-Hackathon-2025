using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio  Source ----------")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("---------- Audio  Clip ----------")]
    public AudioClip shootClip;
    public AudioClip enemyDeathClip;
    public AudioClip computerHurtClip;
    public AudioClip music;

    private void Awake()
    {
        GameObject[] audios = GameObject.FindGameObjectsWithTag("AudioManager");
        if (audios.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
