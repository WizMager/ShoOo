using UnityEngine;
using Views.Impl.Projectile.Interfaces;

namespace Views.Impl.Projectile.Impl
{
    public class PistolBullet : OneTouchProjectile, IForwardDirectionFlyable
    {
        public void Fly(float speed)
        {
            projectileRigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }
    }
}