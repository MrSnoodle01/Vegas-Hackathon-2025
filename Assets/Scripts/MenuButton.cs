using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public InputActionAsset playerActions;
    private InputAction anyKey;
    private bool canInput = false;

    void Start()
    {
        anyKey = playerActions.FindAction("any");
        StartCoroutine("inputCooldown");
    }

    void Update()
    {
        if (anyKey.triggered && canInput)
        {
            GameObject score = GameObject.Find("Score System");
            ScoreSystem scoreSystem = score.GetComponent<ScoreSystem>();
            scoreSystem.score = 0;
            SceneManager.LoadScene("Game");
        }
    }

    IEnumerator inputCooldown()
    {
        yield return new WaitForSeconds(.75f);
        canInput = true;
    }
}
