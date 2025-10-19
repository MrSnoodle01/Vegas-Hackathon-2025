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
    private AudioManager audioManager;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        moveAction = playerActions.FindAction("Move");
        attackAction = playerActions.FindAction("Attack");
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        transform.position += (Vector3)moveInput * Time.deltaTime * speed * enemySpawner.gameSpeed;
        
        if (moveInput != Vector2.zero)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90);
            prevMove = moveInput;
        }

        if (attackAction.triggered && canAttack && prevMove != Vector2.zero)
        {   
            audioManager.playSFX(audioManager.shootClip);

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
        yield return new WaitForSeconds(attackCooldownTime / enemySpawner.gameSpeed);
        canAttack = true;
    }
}
