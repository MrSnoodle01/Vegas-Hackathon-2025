using UnityEngine;

public class Computer : MonoBehaviour
{
    public float health = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //health -= 1f;
            Destroy(collision.gameObject);
            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }   

        if(collision.gameObject.CompareTag("Hacker Projectile"))
        {
            //health -= 1f;
            Destroy(collision.gameObject.transform.parent.gameObject);
            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
