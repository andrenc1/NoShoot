using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject XPOrb;

    [Header("Timer")]
    public float matchDuration = 600f;

    [Header("Game Over")]
    public GameObject gameOverCanvas;
    public TextMeshProUGUI elapsedTimeText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI enemiesText;

    private int enemiesCount;
    private float elapsedTime;
    private bool finished;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (finished) return;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= matchDuration)
        {
            finished = true;
            Win();
        }
    }

    void Win()
    {
        Debug.Log("You Win!");
    }

    public void RestartTimer()
    {
        elapsedTime = 0f;
        finished = false;
    }

    public string GetElapsedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    #region RunStats

    public void EnemyCount()
    {
        enemiesCount++;
    }

    public void GameOver()
    {
        if (finished) return;
        finished = true;

        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        elapsedTimeText.text = GetElapsedTime();
        levelText.text = PlayerController.Instance.GetComponent<PlayerXP>().level.ToString();
        enemiesText.text = enemiesCount.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
