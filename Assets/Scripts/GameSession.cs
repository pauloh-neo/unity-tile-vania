using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numberGameSession = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

        if (numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    
    void Update()
    {
        
    }
    public void ProcessPlayerDeath()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (playerLives > 1)
        {
            TakeLife();
            SceneManager.LoadScene(currentSceneIndex);
            livesText.text = playerLives.ToString();
        }
        else
        {
            ResetGameStatus();
        }
    }
    
    public void Score()
    {
        score += 100;
        scoreText.text = score.ToString();
    }

    private void ResetGameStatus()
    {
        SceneManager.LoadScene(0);
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
    }

    
}
