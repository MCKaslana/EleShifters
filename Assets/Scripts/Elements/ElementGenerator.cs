using UnityEngine;
using System.Collections.Generic;

public class ElementGenerator : Singleton<ElementGenerator>
{
    protected override bool PersistBetweenScenes => false;

    [Header("All Elements")]
    [SerializeField] private List<ElementData> _elements = new();

    [Header("Visual Feedback Bar")]
    [SerializeField] private Transform _confirmTimerBar;
    private Vector3 _timerBarOriginalScale;

    [Header("Spawn Settings")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _confirmTimeLimit = 5f;

    private ElementData _currentGeneratedElement;
    private GameObject _spawnedObject;
    [SerializeField] private GameObject _interfaceObject;

    private float _timer;
    private bool _waitingForConfirm;

    [Header("Difficulty Settings")]

    [SerializeField] private float _minConfirmTime = 2f;
    [SerializeField] private float _confirmTimeLimitModifier = 0.1f;
    [SerializeField] private bool _enableExponentialDifficulty = false;

    private float _confirmTimeLimitOriginal;
    private int _confirmCount = 0;

    protected override void Awake()
    {
        base.Awake();
        _confirmTimeLimitOriginal = _confirmTimeLimit;
        _timerBarOriginalScale = _confirmTimerBar.localScale;
    }

    private void Update()
    {
        if (!_waitingForConfirm)
        {
            if (_currentGeneratedElement == null)
                GenerateNewElement();
            return;
        }

        _timer -= Time.deltaTime;

        if (_confirmTimerBar != null)
        {
            float scaleX = Mathf.Clamp01(_timer / _confirmTimeLimit);
            _confirmTimerBar.localScale = new Vector3(
                scaleX * _timerBarOriginalScale.x,
                _timerBarOriginalScale.y,
                _timerBarOriginalScale.z
            );
        }

        if (_timer <= 0f)
        {
            LoseLife();
            ResetGenerator();
        }
    }

    private void UpdateConfirmTimeLimit()
    {
        if (_enableExponentialDifficulty)
        {
            _confirmTimeLimit = Mathf.Max(_minConfirmTime,
            _confirmTimeLimitOriginal * Mathf.Pow(0.95f, _confirmCount));
        }
        else
        {
            _confirmTimeLimit = Mathf.Max(_minConfirmTime,
            _confirmTimeLimitOriginal - _confirmCount * _confirmTimeLimitModifier);
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

        if (_confirmTimerBar != null)
            _confirmTimerBar.localScale = _timerBarOriginalScale;

        _confirmCount++;
        UpdateConfirmTimeLimit();
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
