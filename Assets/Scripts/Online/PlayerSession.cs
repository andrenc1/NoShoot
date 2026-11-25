using UnityEngine;

public static class PlayerSession
{
    private const string TokenKey = "noshoot.token";
    private const string NameKey = "noshoot.playerName";

    public static bool IsRegistered => PlayerPrefs.HasKey(TokenKey);
    public static string Token => PlayerPrefs.GetString(TokenKey, "");
    public static string PlayerName => PlayerPrefs.GetString(NameKey, "");

    public static void Save(string playerName, string token)
    {
        PlayerPrefs.SetString(NameKey, playerName);
        PlayerPrefs.SetString(TokenKey, token);
        PlayerPrefs.Save();
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteKey(TokenKey);
        PlayerPrefs.DeleteKey(NameKey);
        PlayerPrefs.Save();
    }
}