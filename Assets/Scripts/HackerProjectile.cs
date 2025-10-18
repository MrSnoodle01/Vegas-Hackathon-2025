using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HackerProjectile : MonoBehaviour
{
    public float speed = 5f;

    private GameObject computer;

    private void Awake()
    {
        computer = GameObject.FindWithTag("Computer");
    }

    private void Start()
    {
        StartCoroutine(deleteProjectile());
    }

    void Update()
    {
        if (computer != null)
        {
            Vector3 direction = (computer.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator deleteProjectile()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
