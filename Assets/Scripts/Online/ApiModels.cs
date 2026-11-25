using System;

[Serializable]
public class RegisterPlayerRequest
{
    public string name;
}

[Serializable]
public class RegisterPlayerResponse
{
    public int playerId;
    public string name;
    public string token;
}

[Serializable]
public class SubmitScoreRequest
{
    public int kills;
    public int survivedSeconds;
}

[Serializable]
public class ScoreResponse
{
    public int id;
    public string playerName;
    public int kills;
    public int survivedSeconds;
    public int score;
    public string createdAt;
}

[Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int bestScore;
}

[Serializable]
public class LeaderboardList
{
    public LeaderboardEntry[] items;
}