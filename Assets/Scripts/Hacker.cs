using System.Collections;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    public GameObject projectilePrefab;

    private Computer computer;
    private AudioManager audioManager;
    private string position;
    private EnemySpawner enemySpawner;


    private void Awake()
    {
        computer = GameObject.FindGameObjectWithTag("Computer").GetComponent<Computer>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void Start()
    {
        StartCoroutine(shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            audioManager.playSFX(audioManager.enemyDeathClip);
            enemySpawner.removeHackerPosition(position);
            Destroy(gameObject);
        }
    }

    public void Init(string newPosition)
    {
        position = newPosition;
        switch (newPosition)
        {
            case "botLeft":
                transform.position = new Vector3(-7, -4, 0);
                break;
            case "botRight":
                transform.position = new Vector3(7, -4, 0);
                break;
            case "topLeft":
                transform.position = new Vector3(-7, 4, 0);
                break;
            case "topRight":
                transform.position = new Vector3(7, 4, 0);
                break;
        }
    }

    IEnumerator shoot()
    {
        while(computer.health > 0)
        {
            yield return new WaitForSeconds(3);
            Quaternion rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.up, transform.position - computer.transform.position));
            Instantiate(projectilePrefab, transform.position, rotation);
        }
    }
}
