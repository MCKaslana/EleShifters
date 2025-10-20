using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ElementManager : MonoBehaviour
{
    [Header("Element Inputs")]
    [SerializeField] private List<ElementData> _elementList = new();
    [SerializeField] private List<Image> _elementSprites = new();
    private int _maximumElementCount = 4;

    private ElementData _activeElement;
    private int _activeIndex = -1;

    private float _defaultOpacity = 0.30f;
    private float _selectedElementOpacity = 1f;

    private System.Action _onUp, _onRight, _onDown, _onLeft;

    private void Start()
    {
        _onUp = () => SelectElementByIndex(0);
        _onDown = () => SelectElementByIndex(1);
        _onRight = () => SelectElementByIndex(2);
        _onLeft = () => SelectElementByIndex(3);

        InputManager.Instance.OnInputUp += _onUp;
        InputManager.Instance.OnInputRight += _onRight;
        InputManager.Instance.OnInputDown += _onDown;
        InputManager.Instance.OnInputLeft += _onLeft;
        InputManager.Instance.OnConfirmed += ConfirmedElement;

        foreach (var image in _elementSprites)
            SetOpacity(image, _defaultOpacity);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInputUp -= () => SelectElementByIndex(0);
        InputManager.Instance.OnInputRight -= () => SelectElementByIndex(1);
        InputManager.Instance.OnInputDown -= () => SelectElementByIndex(2);
        InputManager.Instance.OnInputLeft -= () => SelectElementByIndex(3);
        InputManager.Instance.OnConfirmed -= ConfirmedElement;
    }

    private void SelectElementByIndex(int index)
    {
        if (index < 0 || index >= _elementList.Count)
            return;

        _activeIndex = index;
        _activeElement = _elementList[index];

        for (int i = 0; i < _elementSprites.Count; i++)
        {
            float opacity = (i == index) ? _selectedElementOpacity : _defaultOpacity;
            SetOpacity(_elementSprites[i], opacity);
        }
    }

    private void SetOpacity(Image sprite, float opacity)
    {
        if (sprite == null) return;
        Color c = sprite.color;
        c.a = opacity;
        sprite.color = c;
    }

    private void ConfirmedElement()
    {
        if (_activeElement == null)
            return;

        var opponent = ElementGenerator.Instance.GetCurrentGeneratedElement();
        if (opponent == null)
            return;

        Debug.Log($"Confirmed element: {_activeElement.name}");
        ReactionManager.Instance.React(_activeElement, opponent);

        ElementGenerator.Instance.ConfirmElement();
    }

    public void RegisterElement(ElementData element)
    {
        if (_elementList.Contains(element) || _elementList.Count >= _maximumElementCount) return;
        _elementList.Add(element);
    }
}
