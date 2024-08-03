using R3;
using Utils.ObjectPool;
using Views.Impl.Projectile.Impl;

namespace Utils.Weapons.Impl
{
    public class PistolWeapon : AWeapon
    {
        private ProjectilePool<PistolBullet> _weaponProjectilePool;

        private readonly CompositeDisposable _disposable = new ();

        public override void Initialize()
        {
            _weaponProjectilePool = new ProjectilePool<PistolBullet>(projectilePrefab);
        }

        public override void Shoot()
        {
            var projectile = _weaponProjectilePool.GetProjectile();

            projectile.transform.position = projectileShootPoint.position;
            projectile.transform.rotation = projectileShootPoint.rotation;
            
            projectile.ActivateProjectile(damage);
            projectile.Fly(projectileSpeed, transform.forward);

            projectile.ExistProjectileEnded.Subscribe(_ => OnExistProjectileEnded(projectile)).AddTo(_disposable);
        }

        private void OnExistProjectileEnded(PistolBullet pistolBullet)
        {
            _weaponProjectilePool.ReleaseProjectile(pistolBullet);
        }
    }
}