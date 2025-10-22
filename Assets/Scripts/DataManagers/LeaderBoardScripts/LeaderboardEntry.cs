using System;

[Serializable]
public class LeaderboardEntry
{
    public string PlayerName;
    public int Score;
    public float TimeSurvived;

    public LeaderboardEntry(string name, int score, float time)
    {
        PlayerName = name;
        Score = score;
        TimeSurvived = time;
    }
}
