using System;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _leaderboardText;

    private const int MaxEntries = 8;
    private string _filePath;
    private LeaderboardData _leaderboardData = new();

    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        Debug.Log($"Leaderboard JSON file path: {_filePath}");
    }

    private void Start()
    {
        LoadLeaderboard();
        TryAddNewEntry();
        DisplayLeaderboard();
        SaveLeaderboard();
    }

    private void TryAddNewEntry()
    {
        var data = PlayerDataManager.Instance;
        if (data == null) return;

        string name = "P";
        _leaderboardData.entries.Add(new LeaderboardEntry(name, data.PlayerScore, data.TimeSurvived));

        _leaderboardData.entries = _leaderboardData.entries
            .OrderByDescending(e => e.Score)
            .ThenByDescending(e => e.TimeSurvived)
            .Take(MaxEntries)
            .ToList();
    }

    private void DisplayLeaderboard()
    {
        if (_leaderboardText == null) return;

        _leaderboardText.text = "";
        int rank = 1;
        foreach (var entry in _leaderboardData.entries)
        {
            _leaderboardText.text +=
                $"{rank}. {entry.PlayerName} - {entry.Score} pts ({entry.TimeSurvived:F1}s)\n";
            rank++;
        }
    }

    private void SaveLeaderboard()
    {
        try
        {
            string json = JsonUtility.ToJson(_leaderboardData, true);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save leaderboard JSON: {ex.Message}");
        }
    }

    private void LoadLeaderboard()
    {
        if (!File.Exists(_filePath))
        {
            _leaderboardData = new LeaderboardData();
            SaveLeaderboard();
            return;
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            _leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);

            if (_leaderboardData == null || _leaderboardData.entries == null)
                _leaderboardData = new LeaderboardData();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load leaderboard JSON: {ex.Message}");
            _leaderboardData = new LeaderboardData();
        }
    }
}