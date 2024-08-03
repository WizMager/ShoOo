using R3;
using Utils.ObjectPool;
using Views.Impl.Projectile.Impl;

namespace Utils.Weapons.Impl
{
    public class GausRifleWeapon : AWeapon
    {
        private ProjectilePool<GausRifleProjectile> _weaponProjectilePool;

        private readonly CompositeDisposable _disposable = new ();

        public override void Initialize()
        {
            _weaponProjectilePool = new ProjectilePool<GausRifleProjectile>(projectilePrefab);
        }

        public override void Shoot()
        {
            var projectile = _weaponProjectilePool.GetProjectile();

            projectile.transform.position = projectileShootPoint.position;
            projectile.transform.rotation = projectileShootPoint.rotation;
            
            projectile.ActivateProjectile(damage);
            projectile.Fly(projectileSpeed, transform. forward);

            projectile.ExistProjectileEnded.Subscribe(_ => OnExistProjectileEnded(projectile)).AddTo(_disposable);
        }

        private void OnExistProjectileEnded(GausRifleProjectile gausRifleProjectile)
        {
            _weaponProjectilePool.ReleaseProjectile(gausRifleProjectile);
        }
    }
}