using UnityEngine;

public class TimerScript : MonoBehaviour
{
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
    }

    public void EnableTImer() => _isRunning = true;
    public void DisableTImer() => _isRunning = false;
    public void ResetTimer() => _timeElapsed = 0f;
}
