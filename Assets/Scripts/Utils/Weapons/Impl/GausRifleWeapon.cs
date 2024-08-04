using System;
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
            
            Observable.Timer(TimeSpan.FromSeconds(0.2)).Subscribe(_ =>
            {
                foreach (var projectile in _weaponProjectilePool.GetAllAvailableProjectiles())
                {
                    projectile.ExistProjectileEnded.Subscribe(_ => OnExistProjectileEnded(projectile)).AddTo(_disposable);
                }
            });//TODO: remove timer, change initialize subscribtion
        }

        public override void Shoot()
        {
            var (isNewProjectile, projectileView) = _weaponProjectilePool.GetProjectile();
            
            if (isNewProjectile)
            {
                projectileView.ExistProjectileEnded.Subscribe(_ => OnExistProjectileEnded(projectileView)).AddTo(_disposable);
            }

            projectileView.transform.position = projectileShootPoint.position;
            projectileView.transform.rotation = projectileShootPoint.rotation;
            
            projectileView.ActivateProjectile(damage);
            projectileView.Fly(projectileSpeed, projectileShootPoint.forward);
        }

        private void OnExistProjectileEnded(GausRifleProjectile gausRifleProjectile)
        {
            _weaponProjectilePool.ReleaseProjectile(gausRifleProjectile);
        }
    }
}