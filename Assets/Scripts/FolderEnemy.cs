using UnityEngine;
using UnityEngine.UIElements;

public class FolderEnemy : MonoBehaviour
{
    public Camera mainCamera;
    public Transform target;
    public float speed = 5f; 

    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if(target == null)
        {
            GameObject computer = GameObject.FindGameObjectWithTag("Computer");
            target = computer.transform;
        }

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
    }

    private void moveToRandomPosition()
    {
        Vector2 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //Vector2 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector2(1, 1));
        //Vector2 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));

        float randomX = Random.Range(screenTopRight.x, screenBottomLeft.x);
        float randomY = Random.Range(screenTopRight.y, screenBottomLeft.y);

        Vector2 randomPosition = new Vector2(randomX, randomY);
        transform.position = randomPosition;
    }
}
