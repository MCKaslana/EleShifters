using UnityEngine;
using System.Collections.Generic;

public class ElementGenerator : Singleton<ElementGenerator>
{
    [Header("All Elements")]
    [SerializeField] private List<ElementData> _elements = new();
    private ElementData _currentGeneratedElement;
    private int _elementIndex;

    private bool _hasGenerated = false;
    private bool _hasInteracted = false;

    private void Update()
    {
        if (_hasGenerated)
        {
            return;
        }
        else
        {
            _currentGeneratedElement = GetRandomElement();
            _hasGenerated = true;
        }
    }

    public void ResetGenerator() => _hasGenerated = false;

    private ElementData GetRandomElement()
    {
        _elementIndex = Random.Range(0, _elements.Count - 1);
        return _elements[_elementIndex];
    }

    public ElementData GetCurrentGeneratedElement()
    {
        return _currentGeneratedElement;
    }
}
