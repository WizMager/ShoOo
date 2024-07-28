using UnityEngine;
using Views.Impl.Projectile.Impl.BaseVariants;
using Views.Impl.Projectile.Interfaces;

namespace Views.Impl.Projectile.Impl
{
    public class ShotgunBullet : OneTouchProjectile, ICustomDirectionFlyable
    {
        public void Fly(float speed, Vector3 direction)
        {
            projectileRigidbody.AddForce(direction * speed, ForceMode.Acceleration);
        }
    }
}