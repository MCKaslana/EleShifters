using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _comboText;
    [SerializeField] private TextMeshProUGUI _livesCount;

    private TimerScript _timerScript;

    private void Awake()
    {
        _timerScript = GetComponent<TimerScript>();
        GameManager.Instance.OnPlayerLoseLife += UpdateLives;
        GameManager.Instance.OnPlayerGainScore += UpdateScore;
        GameManager.Instance.OnPlayerGainCombo += UpdateCombo;
        _timerScript.OnTimeChanged += UpdateTimer;
    }

    private void Start()
    {
        if (_scoreText == null || 
            _timerText == null || 
            _comboText == null || 
            _livesCount == null)
        {
            Debug.LogError("UserInterface: One or more UI Text components are not assigned.");
            return;
        }

        UpdateScore(0);
        UpdateCombo(0);
        UpdateTimer("0");
        UpdateLives(3);
        _timerScript.EnableTImer();
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = $"< {score} >";
    }

    public void UpdateTimer(string timeElapsed)
    {
        _timerText.text = $"< {_timerScript.GetFormattedTime()} >";
    }

    public void UpdateCombo(int combo)
    {
        _comboText.text = $"< {combo} >";
    }

    public void UpdateLives(int livesLeft)
    {
        _livesCount.text = $"< {livesLeft} >";
        UpdateCombo(0);
    }
}
