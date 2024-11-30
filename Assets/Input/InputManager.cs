using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static bool Shoot;
    public static bool Swap;
    public static bool Talk;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _talkAction;
    private InputAction _swapAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _shootAction = _playerInput.actions["Shoot"];
        _swapAction = _playerInput.actions["Swap"];
        _talkAction = _playerInput.actions["Talk"];
    }

    void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        Shoot = _shootAction.ReadValue<float>() > 0;
        Swap = Mouse.current.rightButton.wasPressedThisFrame;
        //Talk = _talkAction.ReadValue<float>() > 0;
        Talk = Keyboard.current.eKey.wasPressedThisFrame;
        if (Talk){
            Debug.Log(Talk);
        }
    }
}
