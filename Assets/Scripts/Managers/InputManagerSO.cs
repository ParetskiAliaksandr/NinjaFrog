using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManagerSO", menuName = "NewInputSystemSO/InputManagerSO", order = 51)]
public class InputManagerSO : DescriptionBaseSO, PlayerController.IMovementActions, PlayerController.IUIActions
{
    private PlayerController _playerController;

    public event Action<float> OnCharacterMove;
    public event Action OnCharacterJump;

    private void OnEnable()
    {
        _playerController = new PlayerController();

        _playerController.Enable();
        _playerController.Movement.Enable();
        _playerController.UI.Enable();

        _playerController.Movement.SetCallbacks(this);
        _playerController.UI.SetCallbacks(this);
    }

    public void EnableMovementActionMap()
    {
        _playerController.Movement.Enable();

        Debug.Log("Movement action map is Enable");
    }

    public void DisableMovementActionMap()
    {
        _playerController.Movement.Disable();

        Debug.Log("Movement action map is Disable");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnCharacterJump.Invoke();
        }
    }

    public void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            OnCharacterMove.Invoke(_playerController.Movement.HorizontalMovement.ReadValue<float>());
        }
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        
    }

    private void OnDisable()
    {
        _playerController.Movement.RemoveCallbacks(this);
        _playerController.UI.RemoveCallbacks(this);

        _playerController.Movement.Disable();
        _playerController.UI.Disable();

        _playerController.Disable();
    }
}
