using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int score = 0;

    TextMeshProUGUI livesText;
    TextMeshProUGUI scoreText;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        livesText = GameObject.Find("Lives Text").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        livesText.text = "Lives: " + playerLives.ToString();
        scoreText.text = "Coins: " + score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    
    void TakeLife()
    {
        playerLives--;
        livesText.text = "Lives: " + playerLives.ToString();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void ResetGameSession()
    {
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void AddToScore(int pointsToAdd)
{
    score += pointsToAdd;
    Debug.Log("Score is now: " + score);
    scoreText.text = "Coins: " + score.ToString();
}
}
