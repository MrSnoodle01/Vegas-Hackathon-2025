using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputActionAsset playerActions;
    public GameObject playerProjectile;
    public float speed = 5f;
    public float attackCooldownTime = .3f;

    private InputAction moveAction;
    private InputAction attackAction;
    private Vector2 prevMove = Vector2.zero;
    private bool canAttack = true;

    void Start()
    {
        moveAction = playerActions.FindAction("Move");
        attackAction = playerActions.FindAction("Attack");
    }

    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        transform.position += (Vector3)moveInput * Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90);
        if (moveInput != Vector2.zero)
        {
            prevMove = moveInput;
        }

        if (attackAction.triggered && canAttack && prevMove != Vector2.zero)
        {   
            canAttack = false;
            StartCoroutine(attackCooldown());

            // spawn projectile
            Vector3 projDirection = new Vector3(transform.position.x, transform.position.y, -1);
            GameObject proj = Instantiate(playerProjectile, projDirection, Quaternion.identity);
            PlayerProjectile projScript = proj.GetComponent<PlayerProjectile>();
            projScript.Init(prevMove);
        }
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;
    }
}
