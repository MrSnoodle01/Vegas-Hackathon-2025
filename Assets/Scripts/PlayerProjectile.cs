using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 5f;
    public bool canPierece = false;

    private Vector2 moveDirection = Vector2.zero;
    private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        StartCoroutine(deleteProjectile());
    }

    void Update()
    {
        if (moveDirection != Vector2.zero)
        {
            transform.position += (Vector3)(moveDirection * speed * Time.deltaTime * enemySpawner.gameSpeed);
        }
    }

    public void Init(Vector2 direction, bool hasPierce)
    {
        moveDirection = direction;
        canPierece = hasPierce;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
    }

    IEnumerator deleteProjectile()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
