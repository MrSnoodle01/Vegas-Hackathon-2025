using UnityEngine;
using UnityEngine.SceneManagement;

public class Computer : MonoBehaviour
{
    public float health = 3f;

    private Animator animator;
    private AudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameObject am = GameObject.FindGameObjectWithTag("AudioManager");
        audioManager = am.GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 1f;
            //audioManager.playSFX(audioManager.computerHurtClip);
            animator.SetFloat("Health", health);
            Destroy(collision.gameObject);
            if (health <= 0f)
            {
                SceneManager.LoadScene("Lose screen");
                Destroy(gameObject);
            }
        }   

        if(collision.gameObject.CompareTag("Hacker Projectile"))
        {
            health -= 1f;
            //audioManager.playSFX(audioManager.computerHurtClip);
            animator.SetFloat("Health", health);
            Destroy(collision.gameObject.transform.parent.gameObject);
            if (health <= 0f)
            {
                SceneManager.LoadScene("Lose screen");
                Destroy(gameObject);
            }
        }
    }
}
