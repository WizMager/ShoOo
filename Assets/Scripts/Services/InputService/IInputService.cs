using UnityEngine;

namespace Services.InputService
{
    public interface IInputService
    {
        bool IsFire { get; }
        public bool IsMoving { get; }
        bool IsAiming { get; }
        public Vector3 MoveDirection { get; }
        Vector3 AimPosition { get; }
        InputActions InputActions { get; }
    }
}