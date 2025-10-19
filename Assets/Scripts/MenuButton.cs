using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public InputActionAsset playerActions;
    private InputAction anyKey;

    void Start()
    {
        anyKey = playerActions.FindAction("any");
    }

    void Update()
    {
        if (anyKey.triggered)
        {
            GameObject score = GameObject.Find("Score System");
            ScoreSystem scoreSystem = score.GetComponent<ScoreSystem>();
            scoreSystem.score = 0;
            SceneManager.LoadScene("Game");
        }
    }
}
