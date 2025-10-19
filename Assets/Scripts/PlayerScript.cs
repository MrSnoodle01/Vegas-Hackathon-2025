using System.Collections;
using Unity.VisualScripting;
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
    private int dualShotCooldownTime = 0;
    private bool hasDualShot = false;
    private int pierceShotCooldownTime = 0; 
    private bool hasPierceShot = false;
    private int autofireCooldownTime= 0;
    private bool hasAutofire = false;

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

        if ((attackAction.triggered && canAttack && prevMove != Vector2.zero && !hasAutofire) || (hasAutofire && canAttack))
        {
            audioManager.playSFX(audioManager.shootClip);

            canAttack = false;
            StartCoroutine(attackCooldown());

            // spawn projectile
            Vector3 projDirection = new Vector3(transform.position.x, transform.position.y, -1);
            GameObject proj = Instantiate(playerProjectile, projDirection, Quaternion.identity);
            PlayerProjectile projScript = proj.GetComponent<PlayerProjectile>();
            projScript.Init(prevMove, hasPierceShot);

            if (hasDualShot)
            {
                Vector3 projDirection2 = new Vector3(transform.position.x, transform.position.y, -1);
                GameObject proj2 = Instantiate(playerProjectile, projDirection2, Quaternion.identity);
                PlayerProjectile projScript2 = proj2.GetComponent<PlayerProjectile>();
                projScript2.Init(prevMove * -1, hasPierceShot);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Dual Shot"))
        {
            dualShotCooldownTime = 15;
            if (!hasDualShot)
            {
                StartCoroutine(dualShotCooldown());
            }
            hasDualShot = true;
            Destroy(collisionObject);
        }
        else if(collisionObject.CompareTag("Pierce Shot"))
        {
            pierceShotCooldownTime = 15;
            if (!hasPierceShot)
            {
                StartCoroutine(pierceShotCooldown());
            }
            hasPierceShot = true;
            Destroy(collisionObject);
        }
        else if (collisionObject.CompareTag("Autofire"))
        {
            autofireCooldownTime = 15;
            if (!hasPierceShot)
            {
                StartCoroutine(autoFireCooldown());
            }
            hasAutofire = true;
            Destroy(collisionObject);
        }
    }

    IEnumerator autoFireCooldown()
    {
        while (autofireCooldownTime > 0)
        {
            yield return new WaitForSeconds(1);
            autofireCooldownTime -= 1;
        }
        hasAutofire = false;
        autofireCooldownTime = 0;
    }

    IEnumerator pierceShotCooldown()
    {
        while (pierceShotCooldownTime > 0)
        {
            yield return new WaitForSeconds(1);
            pierceShotCooldownTime -= 1;
        }
        hasPierceShot = false;
        pierceShotCooldownTime = 0;
    }

    IEnumerator dualShotCooldown()
    {
        while (dualShotCooldownTime > 0)
        {
            yield return new WaitForSeconds(1);
            dualShotCooldownTime -= 1;
        }
        hasDualShot = false;
        dualShotCooldownTime = 0;
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime / enemySpawner.gameSpeed);
        canAttack = true;
    }
}
