using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiError
{
    public long StatusCode;
    public string Body;
    public string Message;

    public ApiError(long statusCode, string body, string message)
    {
        StatusCode = statusCode;
        Body = body;
        Message = message;
    }

    public bool IsNetworkError => StatusCode == 0;
}

public class LeaderboardApi : MonoBehaviour
{
    public static LeaderboardApi Instance;

    [SerializeField] private string baseUrl = "http://localhost:5031";
    [SerializeField] private int timeoutSeconds = 10;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(string playerName, Action<RegisterPlayerResponse> onSuccess, Action<ApiError> onError)
    {
        var body = new RegisterPlayerRequest { name = playerName };

        StartCoroutine(Send("POST", "/api/players", JsonUtility.ToJson(body), null,
            json => onSuccess(JsonUtility.FromJson<RegisterPlayerResponse>(json)),
            onError));
    }

    public void SubmitScore(int kills, int survivedSeconds, Action<ScoreResponse> onSuccess, Action<ApiError> onError)
    {
        var body = new SubmitScoreRequest { kills = kills, survivedSeconds = survivedSeconds };

        StartCoroutine(Send("POST", "/api/scores", JsonUtility.ToJson(body), PlayerSession.Token,
            json => onSuccess(JsonUtility.FromJson<ScoreResponse>(json)),
            onError));
    }

    public void GetTop(int limit, Action<LeaderboardEntry[]> onSuccess, Action<ApiError> onError)
    {
        StartCoroutine(Send("GET", $"/api/scores/top?limit={limit}", null, null,
            json => onSuccess(JsonUtility.FromJson<LeaderboardList>("{\"items\":" + json + "}").items),
            onError));
    }

    private IEnumerator Send(string method, string path, string jsonBody, string token,
        Action<string> onSuccess, Action<ApiError> onError)
    {
        using (var request = new UnityWebRequest(baseUrl + path, method))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = timeoutSeconds;

            if (jsonBody != null)
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonBody));
                request.SetRequestHeader("Content-Type", "application/json");
            }

            if (!string.IsNullOrEmpty(token))
                request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess(request.downloadHandler.text);
            else
                onError(new ApiError(request.responseCode, request.downloadHandler.text, request.error));
        }
    }

    // ---------- Testes no editor (botão direito no componente, com o jogo rodando) ----------

    [Header("Teste no editor")]
    [SerializeField] private string testPlayerName = "unity_test";

    [ContextMenu("Teste: cadastrar jogador")]
    private void TestRegister()
    {
        RegisterPlayer(testPlayerName,
            response =>
            {
                PlayerSession.Save(response.name, response.token);
                Debug.Log($"Cadastrado: {response.name} (id {response.playerId})");
            },
            LogError);
    }

    [ContextMenu("Teste: enviar pontuação")]
    private void TestSubmit()
    {
        SubmitScore(30, 60,
            response => Debug.Log($"Enviado: {response.playerName} fez {response.score} pontos"),
            LogError);
    }

    [ContextMenu("Teste: buscar ranking")]
    private void TestTop()
    {
        GetTop(10,
            entries =>
            {
                Debug.Log($"Ranking com {entries.Length} jogadores:");
                foreach (var entry in entries)
                    Debug.Log($"  {entry.playerName}: {entry.bestScore}");
            },
            LogError);
    }

    [ContextMenu("Teste: apagar cadastro local")]
    private void TestClear()
    {
        PlayerSession.Clear();
        Debug.Log("Cadastro local apagado.");
    }

    private void LogError(ApiError error)
    {
        if (error.IsNetworkError)
            Debug.LogError($"Sem conexão com a API: {error.Message}");
        else
            Debug.LogError($"Erro {error.StatusCode}: {error.Body}");
    }
}