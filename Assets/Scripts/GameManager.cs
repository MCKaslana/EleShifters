using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Settings")]
    [SerializeField] private int _playerLives = 3;

    private int _currentPlayerLives;
    private int _playerScore = 0;
    private int _playerCombo = 0;

    public event Action<int> OnPlayerLoseLife;
    public event Action<int> OnPlayerGainScore;
    public event Action<int> OnPlayerGainCombo;

    private bool _isGameRunning = false;

    protected override void Awake()
    {
        base.Awake();
        _currentPlayerLives = _playerLives;
        _isGameRunning = true;
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

        OnPlayerGainScore?.Invoke(_playerScore);
    }

    public void UpdatePlayerLives()
    {
        _currentPlayerLives--;
        OnPlayerLoseLife?.Invoke(_currentPlayerLives);
        ResetCombo();

        if (_currentPlayerLives <= 0)
        {
            _isGameRunning = false;
            SceneTransitioner.Instance.TransitionToScene(3);
        }
    }

    public void GainCombo()
    {
        _playerCombo++;
        OnPlayerGainCombo?.Invoke(_playerCombo);
    }

    private void ResetCombo()
    {
        _playerCombo = 0;
        OnPlayerGainCombo?.Invoke(_playerCombo);
    }
}