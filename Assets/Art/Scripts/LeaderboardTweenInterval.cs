using DG.Tweening;
using UnityEngine;

public class LeaderboardTweenInterval : MonoBehaviour
{
    
    [Header("Variable Settings")]

    [Range(1, 3)]
    [SerializeField] private  int currentIndex = 0;
    [Range(50, 0)]
    [SerializeField] private  float tweenDuration = 0;
    
    [Header("Coordinates")]
    [SerializeField] private  int startXValue = 0;
    [SerializeField] private  int endXValue = 0;

    
    [Header("Group Indexes")]

    [SerializeField] private  GameObject index1;
    [SerializeField] private  GameObject index2;
    [SerializeField] private  GameObject index3;

    void Start()
    {
        transform.DOMoveX(10f, tweenDuration);
    }

  
    void Update()
    {
        
    }
}

