using UnityEngine;
using Views.Impl.Projectile.Impl.BaseVariants;
using Views.Impl.Projectile.Interfaces;

namespace Views.Impl.Projectile.Impl
{
    public class GausRifleProjectile : TouchProjectile, IForwardDirectionFlyable
    {
        public void Fly(float speed)
        {
            projectileRigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }
    }
}