using UnityEngine;

public class FolderEnemy : MonoBehaviour
{   
    public float speed = 5f;
    public GameObject deathAnimation;
    public GameObject dualShotPowerup;
    public GameObject autoFirePowerup;
    public GameObject piercePowerup;

    private Transform target;
    private Camera mainCamera;
    private float spawnOffset = 1f;
    private AudioManager audioManager;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        mainCamera = Camera.main;

        GameObject computer = GameObject.FindGameObjectWithTag("Computer");
        target = computer.transform;

        GameObject am = GameObject.FindGameObjectWithTag("AudioManager");
        audioManager = am.GetComponent<AudioManager>();

        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();

        moveToRandomPosition();
    }

    void Update()
    {
        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * enemySpawner.gameSpeed * Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            audioManager.playSFX(audioManager.enemyDeathClip);

            Instantiate(deathAnimation, transform.position, Quaternion.identity);
            if(collision.gameObject.GetComponent<PlayerProjectile>().canPierece == false)
            {
                Destroy(collision.gameObject);
            }

            int spawnPowerup = Random.Range(0, 11); // 10% chance
            if (spawnPowerup == 0)
            {
                int powerupToSpawn = Random.Range(0, 3);
                if (powerupToSpawn == 0)
                {
                    Instantiate(dualShotPowerup, transform.position, Quaternion.identity);
                }
                else if (powerupToSpawn == 1)
                {
                    Instantiate(autoFirePowerup, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(piercePowerup, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }

    private void moveToRandomPosition()
    {
        Vector2 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));

        Vector2 spawnPosition = Vector2.zero;
        int edgeToSpawn = Random.Range(0, 4);
        switch (edgeToSpawn)
        {
            case 0: // Left Edge
                spawnPosition = new Vector2(screenBottomLeft.x - spawnOffset, Random.Range(screenBottomLeft.y, screenTopRight.y));
                break;
            case 1: // Right Edge
                spawnPosition = new Vector2(screenTopRight.x + spawnOffset, Random.Range(screenBottomLeft.y, screenTopRight.y));
                break;
            case 2: // Bottom Edge
                spawnPosition = new Vector2(Random.Range(screenBottomLeft.x, screenTopRight.x), screenBottomLeft.y - spawnOffset);
                break;
            case 3: // Top Edge
                spawnPosition = new Vector2(Random.Range(screenBottomLeft.x, screenTopRight.x), screenTopRight.y + spawnOffset);
                break;
        }

        transform.position = spawnPosition;

        // rotate to face center
        if(spawnPosition.x > 0)
        {
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }
}
