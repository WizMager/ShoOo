using R3;
using UnityEngine;

namespace Services.InputService
{
    public interface IInputService
    {
        bool IsAttack { get; }
        public bool IsMoving { get; }
        bool IsAiming { get; }
        Observable<Unit> Reloading { get; }
        public Vector3 MoveDirection { get; }
        Vector2 AimPosition { get; }
    }
}