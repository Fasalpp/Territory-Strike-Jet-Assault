using TMPro;
using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    public static ScoreManagement instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateScore();
    }

    void Update()
    {
        
    }
    void UpdateScore()
    {
        if(scoreText != null)
        {
            scoreText.text = "SCORE : " + score;
        }
    }
    public void AddScore(int scores)
    {
        score += scores;
        UpdateScore();
    }
}
