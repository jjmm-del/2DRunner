using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private InputAction _jumpAction;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        _jumpAction = new InputAction(type: InputActionType.Button);
        
        _jumpAction.AddBinding("<Keyboard>/space");
        _jumpAction.AddBinding("<Touchscreen>/Press");
        
        _jumpAction.started += context => _playerMovement.Jump();

    }

    private void OnEnable()
    {
        _jumpAction.Enable();
    }

    private void OnDisable()
    {
        _jumpAction.Disable();
    }

    private void OnDestroy()
    {
        _jumpAction.started -= context => _playerMovement.Jump();
        _jumpAction.Dispose();
    }
}
