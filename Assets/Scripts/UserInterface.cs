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
        _timerScript.OnTimeChanged += UpdateTimer;
    }

    private void Start()
    {
        //UpdateScore(0);
        //UpdateCombo(0);
        UpdateTimer("0");
        //UpdateLives(0);
        _timerScript.EnableTImer();
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = $"< {score} >";
    }

    public void UpdateTimer(string timeElapsed)
    {
        _timerText.text = $"< {_timerScript.GetTime()} >";
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
