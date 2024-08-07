using System;
using R3;
using UnityEngine;

namespace Services.InputService.Impl
{
    public class InputService : IInputService, IDisposable
    {
        public bool IsAttack { get; private set; }
        public bool IsMoving { get; private set; }
        public bool IsAiming { get; private set; }
        public Observable<Unit> Reloading => _reloadingCommand;
        public Vector3 MoveDirection
        {
            get
            {
                var direction = _inputActions.KeyboardAndMouse.Movement.ReadValue<Vector2>();
                return new Vector3(direction.x, 0, direction.y);
            }
        }
        public Vector2 AimPosition => _inputActions.KeyboardAndMouse.PointerPosition.ReadValue<Vector2>();

        private readonly InputActions _inputActions;
        private readonly ReactiveCommand<Unit> _reloadingCommand = new();

        public InputService()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
            
            _inputActions.KeyboardAndMouse.Attack.performed += _ => IsAttack = true;
            _inputActions.KeyboardAndMouse.Attack.canceled += _ => IsAttack = false;

            _inputActions.KeyboardAndMouse.Movement.started += _ => IsMoving = true;
            _inputActions.KeyboardAndMouse.Movement.canceled += _ => IsMoving = false;
            
            _inputActions.KeyboardAndMouse.IsAiming.started += _ => IsAiming = true;
            _inputActions.KeyboardAndMouse.IsAiming.canceled += _ => IsAiming = false;
            
            _inputActions.KeyboardAndMouse.Reload.performed += _ => _reloadingCommand.Execute(default);
        }
        
        public void Dispose()
        {
            _inputActions.KeyboardAndMouse.Attack.Dispose();
            _inputActions.KeyboardAndMouse.Movement.Dispose();
            
            _inputActions.Disable();
        }
    }
}