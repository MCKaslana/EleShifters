using System;

[Serializable]
public class LeaderboardEntry
{
    public string PlayerName;
    public int PerfectScore;
    public int Score;
    public float TimeSurvived;

    public LeaderboardEntry(string name, int perfectScore, int score, float time)
    {
        PlayerName = name;
        PerfectScore = perfectScore;
        Score = score;
        TimeSurvived = time;
    }
}
