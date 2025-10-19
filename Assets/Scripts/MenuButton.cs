using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public InputActionAsset playerActions;
    private InputAction anyKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anyKey = playerActions.FindAction("any");
    }

    // Update is called once per frame
    void Update()
    {
        if (anyKey.triggered)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void playGame()
    {
        SceneManager.LoadScene("Game");
    }
}
