using System.Collections;
using UnityEngine;

public class HackerProjectile : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;

    private GameObject computer;
    private string animationStateName = "Moving";
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        computer = GameObject.FindWithTag("Computer");

        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    private void Start()
    {
        StartCoroutine(deleteProjectile());
        animator.Play(animationStateName, -1, Random.Range(0f, 0.9f));
        if(transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.up, transform.position - computer.transform.position));
        }
        else
        {
            if(transform.position.y > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }     
        }       
    }

    void Update()
    {
        if (computer != null)
        {
            Vector3 direction = (computer.transform.position - transform.position).normalized;
            transform.position += direction * speed * enemySpawner.gameSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    IEnumerator deleteProjectile()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject.transform.parent);
    }
}
