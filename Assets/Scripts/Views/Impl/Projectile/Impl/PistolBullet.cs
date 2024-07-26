using UnityEngine;
using Views.Impl.Projectile.Interfaces;

namespace Views.Impl.Projectile.Impl
{
    public class PistolBullet : OneTouchProjectile, IFlyable
    {
        [SerializeField] private Rigidbody projectileRigidbody;
        
        public void Fly(float speed)
        {
            projectileRigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }

        public override void ResetProjectile()
        {
            projectileRigidbody.velocity = Vector3.zero;
            
            base.ResetProjectile();
        }
    }
}