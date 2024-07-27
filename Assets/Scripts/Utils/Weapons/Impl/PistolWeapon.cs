using Utils.ObjectPool;
using Views.Impl.Projectile.Impl;

namespace Utils.Weapons.Impl
{
    public class PistolWeapon : AWeapon
    {
        private ProjectilePool<PistolBullet> _weaponProjectilePool;

        public override void Initialize()
        {
            _weaponProjectilePool = new ProjectilePool<PistolBullet>(projectilePrefab);
        }

        public override void Shoot()
        {
            var projectile = _weaponProjectilePool.GetProjectile();

            projectile.transform.position = projectileShootPoint.position;
            projectile.transform.rotation = projectileShootPoint.rotation;
            
            projectile.ActivateProjectile();
            projectile.Fly(projectileSpeed);
        }
    }
}