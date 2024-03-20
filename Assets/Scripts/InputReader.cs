using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActionsActions
{
    private PlayerInput _playerInput;

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();

            _playerInput.PlayerActions.SetCallbacks(this);

            SetPlayerActions();
        }
    }

    public void SetPlayerActions()
    {
        _playerInput.PlayerActions.Enable();
        //shoto tam etc disabled, same thing with others mappings setphase of game
    }

    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action ToggleViewEvent;
    public event Action JumpEvent;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnToggleView(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            ToggleViewEvent?.Invoke();
        }
    }
}