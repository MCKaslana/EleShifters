using UnityEngine;
using System.Collections.Generic;

public class ElementManager : MonoBehaviour
{
    private List<Element> _elementList = new();
    private int _maximumElementCount = 4;
    private Element _activeElement;

    private void Start()
    {
        /*InputManager.Instance.OnInputUp += SwapActiveElement;
        InputManager.Instance.OnInputDown += SwapActiveElement;
        InputManager.Instance.OnInputLeft += SwapActiveElement;
        InputManager.Instance.OnInputRight += SwapActiveElement;*/
    }

    private void OnDisable()
    {
        //InputManager.Instance.OnInputUp -= TopElement;
    }

    public void RegisterElement(Element element)
    {
        if (_elementList.Contains(element) || _elementList.Count >= _maximumElementCount) return;
        _elementList.Add(element);
    }

    public void SwapActiveElement(Element newElement)
    {
        _activeElement = newElement;
    }


}
