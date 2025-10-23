using System;

public class InputManager : Singleton<InputManager>
{
    protected override bool PersistBetweenScenes => false;

    private PlayerInputSystem _inputActions;

    public event Action OnInputUp;
    public event Action OnInputDown;
    public event Action OnInputLeft;
    public event Action OnInputRight;

    public event Action OnConfirmed;

    protected override void Awake()
    {
        _inputActions = new PlayerInputSystem();

        _inputActions.Player.NavigateUp.performed 
            += ctx => OnUpPressed();
        _inputActions.Player.NavigateDown.performed 
            += ctx => OnDownPressed();
        _inputActions.Player.NavigateLeft.performed
            += ctx => OnLeftPressed();
        _inputActions.Player.NavigateRight.performed
            += ctx => OnRightPressed();

        _inputActions.Player.Confirm.performed 
            += ctx => OnConfirmPressed();

        _inputActions.Player.Enable();
    }

    private void OnDestroy()
    {
        _inputActions.Player.NavigateUp.performed 
            -= ctx => OnUpPressed();
        _inputActions.Player.NavigateDown.performed 
            -= ctx => OnDownPressed();
        _inputActions.Player.NavigateLeft.performed
            -= ctx => OnLeftPressed();
        _inputActions.Player.NavigateRight.performed
            -= ctx => OnRightPressed();

        _inputActions.Player.Confirm.performed 
            -= ctx => OnConfirmPressed();
    }

    private void OnUpPressed()
    {
        OnInputUp?.Invoke();
    }

    private void OnDownPressed()
    {
        OnInputDown?.Invoke();
    }

    private void OnLeftPressed()
    {
        OnInputLeft?.Invoke();
    }

    private void OnRightPressed()
    {
        OnInputRight?.Invoke();
    }

    private void OnConfirmPressed()
    {
        OnConfirmed?.Invoke();
    }
}
