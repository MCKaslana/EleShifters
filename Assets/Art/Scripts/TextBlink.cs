using TMPro;
using UnityEngine;

public class TextBlink : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Assign your TextMeshPro component in the Inspector
    public float blinkSpeed = 1f; // Speed of the blinking effect

    private bool isBlinking = true;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (isBlinking)
        {
            // Calculate alpha value oscillating between 0 and 1
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);

            // Update the text's alpha
            Color color = textMeshPro.color;
            color.a = alpha;
            textMeshPro.color = color;
        }
    }

    public void StartBlinking()
    {
        isBlinking = true;
    }

    public void StopBlinking()
    {
        isBlinking = false;

        // Reset alpha to fully visible
        Color color = textMeshPro.color;
        color.a = 1f;
        textMeshPro.color = color;
    }
}