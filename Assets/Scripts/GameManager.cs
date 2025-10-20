using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isGameRunning = false;
    private int _playerLives = 3;
    private int _currentPlayerLives;

    protected override void Awake()
    {
        base.Awake();
        _currentPlayerLives = _playerLives;
        _isGameRunning = true;
    }

    public void UpdatePlayerLives()
    {
        _currentPlayerLives--;
        if (_currentPlayerLives <= 0)
        {
            _isGameRunning = false;
            SceneTransitioner.Instance.TransitionToScene(3);
        }
    }
}