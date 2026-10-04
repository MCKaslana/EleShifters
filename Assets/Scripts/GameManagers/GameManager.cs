using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override bool PersistBetweenScenes => false;

    [Header("Game Settings")]
    [SerializeField] private int _playerLives = 3;
    [SerializeField] private TimerScript _timer;
    [SerializeField] private int _endSceneIndex;

    private float _elapsedTimeSurvived;
    private int _playerScore = 0;
    private int _playerPerfectScore = 0;
    private int _playerCombo = 0;

    private int _highestCombo;

    public int GetPlayerScore() => _playerScore;
    public int GetPerfectScore() => _playerPerfectScore;
    public int GetPlayerCombo() => _highestCombo;
    public float GetElapsedTimeSurvived() => _elapsedTimeSurvived;

    private int _currentPlayerLives;

    public event Action<int> OnPlayerLoseLife;
    public event Action<int> OnPlayerGainScore;
    public event Action<int> OnPlayerGainCombo;

    private bool _hasRecordedPerfectScore = false;

    protected override void Awake()
    {
        base.Awake();
        _currentPlayerLives = _playerLives;
        _hasRecordedPerfectScore = false;
    }

    public void AddScore()
    {
        var score = _currentPlayerLives switch
        {
            3 => 100,
            2 => 90,
            1 => 80,
            _ => 0
        };

        _playerScore += score;
        GainCombo();

        OnPlayerGainScore?.Invoke(_playerScore);
    }

    public void UpdatePlayerLives()
    {
        if (!_hasRecordedPerfectScore)
        {
            _playerPerfectScore = _playerScore;
            _hasRecordedPerfectScore = true;
        }

        _currentPlayerLives--;
        OnPlayerLoseLife?.Invoke(_currentPlayerLives);
        ResetCombo();

        if (_currentPlayerLives <= 0)
        {
            _elapsedTimeSurvived = _timer.GetElapsedTime();
            PlayerDataManager.Instance.SaveFromGameManager();
            SceneTransitioner.Instance.TransitionToScene(_endSceneIndex);
        }
    }

    public void GainCombo()
    {
        _playerCombo++;
        _highestCombo++;
        OnPlayerGainCombo?.Invoke(_playerCombo);
    }

    private void ResetCombo()
    {
        if (_playerCombo >= _highestCombo)
        {
            _highestCombo = _playerCombo;
        }

        _playerCombo = 0;
        OnPlayerGainCombo?.Invoke(_playerCombo);
    }
}