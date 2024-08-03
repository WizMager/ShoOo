using UnityEngine;

namespace Views.Impl.Projectile.Interfaces
{
    public interface IFlyable
    {
        void Fly(float speed, Vector3 direction);
    }
}