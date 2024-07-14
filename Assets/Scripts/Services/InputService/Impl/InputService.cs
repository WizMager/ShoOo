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
                var direction = _inputActions.KeayboardAndMouse.Movement.ReadValue<Vector2>();
                return new Vector3(direction.x, 0, direction.y);
            }
        }
        public Vector3 AimPosition
        {
            get
            {
                var position = _inputActions.KeayboardAndMouse.PoinerPosition.ReadValue<Vector2>();
                return new Vector3(position.x, 0, position.y);
            }
        }
        //TODO; for debug
        public InputActions InputActions => _inputActions;

        private readonly InputActions _inputActions;

        public InputService()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
            
            _inputActions.KeayboardAndMouse.Attack.performed += _ => IsFire = true;
            _inputActions.KeayboardAndMouse.Attack.canceled += _ => IsFire = false;

            _inputActions.KeayboardAndMouse.Movement.started += _ => IsMoving = true;
            _inputActions.KeayboardAndMouse.Movement.canceled += _ => IsMoving = false;
            
            _inputActions.KeayboardAndMouse.IsAiming.started += _ => IsAiming = true;
            _inputActions.KeayboardAndMouse.IsAiming.canceled += _ => IsAiming = false;
        }
        
        public void Dispose()
        {
            _inputActions.KeayboardAndMouse.Attack.Dispose();
            _inputActions.KeayboardAndMouse.Movement.Dispose();
            
            _inputActions.Disable();
        }
    }
}