using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static bool Shoot;
    public static bool Swap;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _swapAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _shootAction = _playerInput.actions["Shoot"];
        _swapAction = _playerInput.actions["Swap"];
    }

    void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        Shoot = _shootAction.ReadValue<float>() > 0;
        Swap = Mouse.current.rightButton.wasPressedThisFrame;
    }
}
