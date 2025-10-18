using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 moveDirection = Vector2.zero;

    void Start()
    {
        StartCoroutine(deleteProjectile());
    }

    void Update()
    {
        if (moveDirection != Vector2.zero)
        {
            transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
        }
    }

    public void Init(Vector2 direction)
    {
        moveDirection = direction;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
    }

    IEnumerator deleteProjectile()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
