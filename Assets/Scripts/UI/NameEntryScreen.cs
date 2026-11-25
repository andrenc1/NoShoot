using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameEntryScreen : MonoBehaviour
{
    private const int MinLength = 4;
    private const int MaxLength = 20;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button playButton;
    [SerializeField] private Button offlineButton;
    [SerializeField] private TextMeshProUGUI messageText;

    private void Start()
    {
        if (PlayerSession.IsRegistered)
        {
            panel.SetActive(false);
            return;
        }

        panel.SetActive(true);
        Time.timeScale = 0f;

        nameInput.characterLimit = MaxLength;
        nameInput.onValidateInput = ValidateChar;
        nameInput.onValueChanged.AddListener(_ => messageText.text = "");

        playButton.onClick.AddListener(OnPlayClicked);
        offlineButton.onClick.AddListener(StartGame);

        offlineButton.gameObject.SetActive(false);
        messageText.text = "";

        nameInput.ActivateInputField();
    }

    private char ValidateChar(string text, int charIndex, char addedChar)
    {
        bool isLetter = (addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z');
        bool isDigit = addedChar >= '0' && addedChar <= '9';

        return isLetter || isDigit || addedChar == '_' ? addedChar : '\0';
    }

    private void OnPlayClicked()
    {
        string playerName = nameInput.text.Trim();

        if (playerName.Length < MinLength)
        {
            messageText.text = $"Your name needs at least {MinLength} characters.";
            return;
        }

        SetBusy(true);
        messageText.text = "Registering...";

        LeaderboardApi.Instance.RegisterPlayer(playerName, OnRegistered, OnRegisterFailed);
    }

    private void OnRegistered(RegisterPlayerResponse response)
    {
        PlayerSession.Save(response.name, response.token);
        StartGame();
    }

    private void OnRegisterFailed(ApiError error)
    {
        SetBusy(false);

        if (error.IsNetworkError)
        {
            messageText.text = "Can't reach the server. You can still play without the leaderboard.";
            offlineButton.gameObject.SetActive(true);
        }
        else if (error.StatusCode == 409)
        {
            messageText.text = "This name is already taken. Try another one.";
        }
        else if (error.StatusCode == 400)
        {
            messageText.text = "Invalid name. Use 4 to 20 letters, numbers or _.";
        }
        else
        {
            messageText.text = $"Unexpected error ({error.StatusCode}). Please try again.";
        }
    }

    private void SetBusy(bool busy)
    {
        playButton.interactable = !busy;
        nameInput.interactable = !busy;
    }

    private void StartGame()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}