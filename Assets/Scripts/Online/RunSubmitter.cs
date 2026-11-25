using TMPro;
using UnityEngine;

public class RunSubmitter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;

    private void Start()
    {
        statusText.text = "";
        GameManager.Instance.RunEnded += OnRunEnded;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RunEnded -= OnRunEnded;
    }

    private void OnRunEnded(bool won)
    {
        if (!PlayerSession.IsRegistered)
        {
            statusText.text = "Offline: this run was not sent to the leaderboard.";
            return;
        }

        statusText.text = "Sending score...";

        GameManager gm = GameManager.Instance;
        LeaderboardApi.Instance.SubmitScore(gm.Kills, gm.SurvivedSeconds, OnSubmitted, OnSubmitFailed);
    }

    private void OnSubmitted(ScoreResponse response)
    {
        statusText.text = $"Score sent as {response.playerName}!";
    }

    private void OnSubmitFailed(ApiError error)
    {
        if (error.IsNetworkError)
        {
            statusText.text = "Couldn't reach the server. This run was not saved.";
        }
        else if (error.StatusCode == 401)
        {
            PlayerSession.Clear();
            statusText.text = "Your player was not found. You'll pick a name again next time.";
        }
        else if (error.StatusCode == 400)
        {
            statusText.text = "The server rejected this run.";
        }
        else
        {
            statusText.text = $"Unexpected error ({error.StatusCode}). This run was not saved.";
        }
    }
}