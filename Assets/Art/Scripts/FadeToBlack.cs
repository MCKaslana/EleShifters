using UnityEngine;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    
    [SerializeField] private GameObject blackoutFader;
    [SerializeField] private float _fadeSpeed = 0.5f;
    void Update()
    {
        
    }

    private IEnumerator FadeBlackOutSquare(float fadeSpeed, bool fadeToBlack = true)
    {
        yield return new WaitForEndOfFrame();
    }
}
