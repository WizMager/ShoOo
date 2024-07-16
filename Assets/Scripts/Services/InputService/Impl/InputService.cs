using System;
using UnityEngine;

namespace Services.InputService.Impl
{
    public class InputService : IInputService, IDisposable
    {
        public bool IsFire { get; private set; }
        public bool IsMoving { get; private set; }
        public bool IsAiming { get; private set; }
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

        public InputService()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
            
            _inputActions.KeyboardAndMouse.Attack.performed += _ => IsFire = true;
            _inputActions.KeyboardAndMouse.Attack.canceled += _ => IsFire = false;

            _inputActions.KeyboardAndMouse.Movement.started += _ => IsMoving = true;
            _inputActions.KeyboardAndMouse.Movement.canceled += _ => IsMoving = false;
            
            _inputActions.KeyboardAndMouse.IsAiming.started += _ => IsAiming = true;
            _inputActions.KeyboardAndMouse.IsAiming.canceled += _ => IsAiming = false;
        }
        
        public void Dispose()
        {
            _inputActions.KeyboardAndMouse.Attack.Dispose();
            _inputActions.KeyboardAndMouse.Movement.Dispose();
            
            _inputActions.Disable();
        }
    }
}