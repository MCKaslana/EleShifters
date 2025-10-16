using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _comboText;
    [SerializeField] private TextMeshProUGUI _livesCount;

    private void Start()
    {
        UpdateScore(0);
        UpdateCombo(0);
        UpdateTimer(0);
        UpdateLives(0);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = $"< {score} >";
    }

    public void UpdateTimer(float timeElapsed)
    {
        _timerText.text = $"< {timeElapsed} >";
    }

    public void UpdateCombo(int combo)
    {
        _comboText.text = $"< {combo} >";
    }

    public void UpdateLives(int livesLeft)
    {
        _livesCount.text = $"< {livesLeft} >";
    }
}
