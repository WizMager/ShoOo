using UnityEngine;

namespace Views.Impl.Projectile.Interfaces
{
    public interface ICustomDirectionFlyable
    {
        void Fly(float speed, Vector3 direction);
    }
}