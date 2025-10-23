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

        ShuffleElements();

        foreach (var image in _elementSprites)
            SetOpacity(image, _defaultOpacity);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInputUp -= _onUp;
        InputManager.Instance.OnInputRight -= _onRight;
        InputManager.Instance.OnInputDown -= _onDown;
        InputManager.Instance.OnInputLeft -= _onLeft;
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

    private void ShuffleElements()
    {
        for (int i = 0; i < _elementList.Count; i++)
        {
            int randomIndex = Random.Range(i, _elementList.Count);
            (_elementList[i], _elementList[randomIndex]) = (_elementList[randomIndex], _elementList[i]);
        }

        UpdateElementVisuals();

        for (int i = 0; i < _elementList.Count; i++)
        {
            Debug.Log($"Slot {i}: {_elementList[i].elementType}");
        }
    }

    private void UpdateElementVisuals()
    {
        for (int i = 0; i < _elementSprites.Count; i++)
        {
            if (i >= _elementList.Count) break;

            var element = _elementList[i];
            var image = _elementSprites[i];

            if (element == null || image == null) continue;

            image.color = new Color(
                element.elementColor.r,
                element.elementColor.g,
                element.elementColor.b,
                _defaultOpacity
            );
        }
    }
}
