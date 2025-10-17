using System;
using UnityEditor.Rendering;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
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
        Debug.Log("Up pressed / Joystick up");
        OnInputUp?.Invoke();
    }

    private void OnDownPressed()
    {
        Debug.Log("Down pressed / Joystick down");
        OnInputDown?.Invoke();
    }

    private void OnLeftPressed()
    {
        Debug.Log("Left pressed / Joystick left");
        OnInputLeft?.Invoke();
    }

    private void OnRightPressed()
    {
        Debug.Log("Right pressed / Joystick right");
        OnInputRight?.Invoke();
    }

    private void OnConfirmPressed()
    {
        Debug.Log("Confirm pressed");
        OnConfirmed?.Invoke();
    }
}
