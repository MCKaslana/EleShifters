using System;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public event Action<string> OnTimeChanged;

    private float _timeElapsed;
    private bool _isRunning = false;

    private void Start()
    {
        _timeElapsed = 0f;
    }

    private void Update()
    {
        if (!_isRunning) return;

        _timeElapsed += Time.deltaTime;
        OnTimeChanged?.Invoke(_timeElapsed.ToString());
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(_timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(_timeElapsed % 60f);
        int milliseconds = Mathf.FloorToInt((_timeElapsed * 100f) % 100f);
        return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    public float GetElapsedTime() => _timeElapsed;

    public void EnableTImer() => _isRunning = true;
    public void DisableTImer() => _isRunning = false;
    public void ResetTimer() => _timeElapsed = 0f;
}
