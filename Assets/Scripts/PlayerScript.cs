using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputActionAsset playerActions;
    public float speed = 5f;

    private InputAction moveAction;
    private InputAction attackAction;
   
    void Start()
    {
        moveAction = playerActions.FindAction("Move");
        attackAction = playerActions.FindAction("Attack");
    }

    void Update()
    {
        transform.position += (Vector3)moveAction.ReadValue<Vector2>() * Time.deltaTime * speed;
    }
}
