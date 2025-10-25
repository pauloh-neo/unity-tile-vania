using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
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
    
    public void ProcessPlayerDeath()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(playerLives > 1)
        {
            TakeLife();
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }

    private void TakeLife()
    {
        playerLives--;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
