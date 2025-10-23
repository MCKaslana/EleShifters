using UnityEngine;
using System.Collections.Generic;

public class ElementGenerator : Singleton<ElementGenerator>
{
    [Header("All Elements")]
    [SerializeField] private List<ElementData> _elements = new();

    [Header("Spawn Settings")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _confirmTimeLimit = 5f;

    private ElementData _currentGeneratedElement;
    private GameObject _spawnedObject;
    [SerializeField] private GameObject _interfaceObject;

    private float _timer;
    private bool _waitingForConfirm;

    private void Update()
    {
        if (!_waitingForConfirm)
        {
            if (_currentGeneratedElement == null)
                GenerateNewElement();
            return;
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            LoseLife();
            ResetGenerator();
        }
    }

    private void GenerateNewElement()
    {
        _currentGeneratedElement = GetRandomElement();

        if (_currentGeneratedElement.elementPrefab != null && _spawnPoint != null)
        {
            _spawnedObject = Instantiate(
                _currentGeneratedElement.elementPrefab,
                _spawnPoint.position,
                Quaternion.identity
            );

            _spawnedObject.transform.parent = _interfaceObject.transform;
            _spawnedObject.transform.position = _spawnPoint.position;
        }

        Debug.Log($"Generated element: {_currentGeneratedElement.elementType}");

        _timer = _confirmTimeLimit;
        _waitingForConfirm = true;
    }

    public void ConfirmElement()
    {
        if (!_waitingForConfirm)
            return;

        _waitingForConfirm = false;
        _timer = 0f;

        if (_spawnedObject != null)
            Destroy(_spawnedObject);

        _spawnedObject = null;
        _currentGeneratedElement = null;
    }

    public void ResetGenerator()
    {
        _waitingForConfirm = false;
        _timer = 0f;

        if (_spawnedObject != null)
            Destroy(_spawnedObject);

        _spawnedObject = null;
        _currentGeneratedElement = null;
    }

    private ElementData GetRandomElement()
    {
        int index = Random.Range(0, _elements.Count);
        return _elements[index];
    }

    public ElementData GetCurrentGeneratedElement() => _currentGeneratedElement;

    private void LoseLife()
    {
        GameManager.Instance.UpdatePlayerLives();
    }
}
