using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _perfectScoreText;
    [SerializeField] private TextMeshProUGUI _comboText;
    [SerializeField] private TextMeshProUGUI _timeText;

    private void Start()
    {
        var data = PlayerDataManager.Instance;

        if (data == null)
        {
            Debug.LogWarning("[ScoreBoard] No PlayerDataManager found.");
            return;
        }

        _scoreText.text = $"{data.PlayerScore}";
        _perfectScoreText.text = $"{data.PlayerPerfectScore}";
        _comboText.text = $"{data.PlayerCombo}";
        _timeText.text = $"{FormatTime(data.TimeSurvived)}";
    }

    private string FormatTime(float seconds)
    {
        int mins = Mathf.FloorToInt(seconds / 60f);
        int secs = Mathf.FloorToInt(seconds % 60f);
        int milliseconds = Mathf.FloorToInt((seconds * 100f) % 100f);
        return $"{mins:00}:{secs:00}:{milliseconds:00}";
    }
}
