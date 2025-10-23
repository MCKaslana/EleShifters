using UnityEngine;

public class DingusSpin : MonoBehaviour
{
    [SerializeField] private int _rotationSpeed = 25;
   
    void Update()
    {
        transform.Rotate(0f, 0f, 1f * _rotationSpeed * Time.deltaTime);
    }
   
   
   
}
