using UnityEditor.Rendering;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private PlayerInputSystem _inputActions;

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
    }

    private void OnDownPressed()
    {
        Debug.Log("Down pressed / Joystick down");
    }

    private void OnLeftPressed()
    {
        Debug.Log("Left pressed / Joystick left");
    }

    private void OnRightPressed()
    {
        Debug.Log("Right pressed / Joystick right");
    }

    private void OnConfirmPressed()
    {
        Debug.Log("Confirm pressed");
    }
}
