using System.Collections;
using UnityEngine;

public class DeathLetters : MonoBehaviour
{
    public Animator animator;

    private string animationStateName = "Moving";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.Play(animationStateName, -1, Random.Range(0f, 0.9f));
        StartCoroutine(deleteLetters());
    }

    IEnumerator deleteLetters()
    {
        yield return new WaitForSeconds(.05f);
        if(name.Contains("Row 0"))
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(0, -.4f, 0);
        yield return new WaitForSeconds(.05f);
        if(name.Contains("Row 1"))
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(0, -.4f, 0);
        yield return new WaitForSeconds(.05f);
        if (name.Contains("Row 2"))
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(0, -.4f, 0);
        yield return new WaitForSeconds(.05f);
        transform.position += new Vector3(0, -.4f, 0);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
