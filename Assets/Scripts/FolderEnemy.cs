using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class FolderEnemy : MonoBehaviour
{   
    public float speed = 5f;

    private Transform target;
    private Camera mainCamera;
    private float spawnOffset = 1f;

    private void Awake()
    {
        mainCamera = Camera.main;

        GameObject computer = GameObject.FindGameObjectWithTag("Computer");
        target = computer.transform;
    }

    void Start()
    {
        moveToRandomPosition();
    }

    void Update()
    {
        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Computer"))
        {
            moveToRandomPosition();
        }
        if(collision.gameObject.CompareTag("Projectile"))
        {
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
