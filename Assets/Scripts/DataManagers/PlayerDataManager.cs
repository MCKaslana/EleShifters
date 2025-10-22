using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    [Header("Player Session Data")]
    public int PlayerScore { get; private set; }
    public int PlayerPerfectScore { get; private set; }
    public int PlayerCombo { get; private set; }
    public float TimeSurvived { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void SaveFromGameManager()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        PlayerScore = gm.GetPlayerScore();
        PlayerPerfectScore = gm.GetPerfectScore();
        PlayerCombo = gm.GetPlayerCombo();
        TimeSurvived = gm.GetElapsedTimeSurvived();

        Debug.Log($"[PlayerDataManager] Saved data: Score={PlayerScore}, Perfect={PlayerPerfectScore}, Combo={PlayerCombo}, Time={TimeSurvived:F2}");
    }
}
