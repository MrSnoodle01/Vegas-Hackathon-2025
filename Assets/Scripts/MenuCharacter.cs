using System.Collections;
using UnityEngine;

public class MenuCharacter : MonoBehaviour
{
    public Animator animator;

    private string animationStateName = "Moving";
    private float speed = 0f;
    private float size = 0f;

    private void Start()
    {
        animator.Play(animationStateName, -1, Random.Range(0f, 0.9f));
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(size, size, size);
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - size, transform.position.z);
            if(transform.position.y < -6)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(speed);
        }
    }

    public void Init(float newSpeed, float newSize)
    {
        speed = newSpeed;
        size = newSize;
    }
}
