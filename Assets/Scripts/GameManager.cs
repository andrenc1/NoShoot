using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject XPOrb;

    [Header("Timer")]
    public float matchDuration = 600f; 

    private float elapsedTime;
    private bool finished;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
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
}
