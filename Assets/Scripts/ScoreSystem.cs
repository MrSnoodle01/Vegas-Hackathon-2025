using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public float score = 0f;

    private TextMeshProUGUI scoreText;
    private string scoreString;

    private void Awake()
    {
        GameObject scoreObject = GameObject.Find("Score text");
        if (scoreObject)
        {
            scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        }

        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    void Update()
    {
        if (scoreText)
        {
            score += 1f;
            scoreString = "";
            for (int i = 0; i < 10 - score.ToString().Length; i++)
            {
                scoreString += "0";
            }
            scoreString += score;
            scoreText.text = scoreString;
        }
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        GameObject scoreObject = GameObject.Find("Score text");
        if (scoreObject)
        {
            scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        }

        GameObject finalScore = GameObject.Find("Final score");
        if (finalScore)
        {
            finalScore.GetComponent<TextMeshProUGUI>().text = "Score: " + scoreString;
        }
    }
}
