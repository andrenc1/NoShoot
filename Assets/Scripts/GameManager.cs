using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject XPOrb;

    [Header("Timer")]
    public float matchDuration;

    [Header("Game Over")]
    public GameObject gameOverCanvas;
    public TextMeshProUGUI elapsedTimeText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI enemiesText;
    public GameObject gameOverPanel;
    public GameObject resultsPanel;

    [Header("Score")]
    public int pointsPerKill;
    public int pointsPerSecond;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI titleText;

    private int enemiesCount;
    private float elapsedTime;
    private bool finished;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        if (finished) return;
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= matchDuration)
        {
            elapsedTime = matchDuration;
            EndRun(true);
        }
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

    public int Kills => enemiesCount;
    public int SurvivedSeconds => Mathf.FloorToInt(elapsedTime);
    public int Score => Kills * pointsPerKill + SurvivedSeconds * pointsPerSecond;

    public void GameOver()
    {
        EndRun(false);
    }

    void EndRun(bool won)
    {
        if (finished) return;
        finished = true;
        Time.timeScale = 0f;

        titleText.text = won ? "VOCÊ VENCEU!" : "GAME OVER";
        elapsedTimeText.text = GetElapsedTime();
        levelText.text = PlayerController.Instance.GetComponent<PlayerXP>().level.ToString();
        enemiesText.text = Kills.ToString();
        scoreText.text = Score.ToString();

        gameOverCanvas.SetActive(true);
        gameOverPanel.SetActive(true);
        resultsPanel.SetActive(false);

        Debug.Log($"Fim da partida | venceu={won} | kills={Kills} | segundos={SurvivedSeconds} | score={Score}");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
