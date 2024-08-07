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
            _weaponProjectilePool = new ProjectilePool<GausRifleProjectile>(projectilePrefab, MagazineSize);
            
            _weaponProjectilePool.ProjectileInstantiated.Subscribe(OnProjectileInstantiated).AddTo(_disposable);

            CurrentProjectilesInMagazine = MagazineSize;
        }

        private void OnProjectileInstantiated(GausRifleProjectile[] projectiles)
        {
            foreach (var projectile in projectiles)
            {
                projectile.ExistProjectileEnded.Subscribe(_ => OnExistProjectileEnded(projectile)).AddTo(_disposable);
            }
        }
        
        public override void Shoot()
        {
            if (IsReloading)
                return;
            
            if (CurrentProjectilesInMagazine <= 0)
            {
                IsReloading = true;
                Reloading().Forget();
                return;
            }

            CurrentProjectilesInMagazine--;
            
            var (isNewProjectile, projectileView) = _weaponProjectilePool.GetProjectile();
            
            if (isNewProjectile)
            {
                projectileView.ExistProjectileEnded.Subscribe(_ => OnExistProjectileEnded(projectileView)).AddTo(_disposable);
            }

            projectileView.transform.position = projectileShootPoint.position;
            projectileView.transform.rotation = projectileShootPoint.rotation;
            
            projectileView.ActivateProjectile(Damage);
            projectileView.Fly(ProjectileSpeed, projectileShootPoint.forward);
        }
        
        private void OnExistProjectileEnded(GausRifleProjectile gausRifleProjectile)
        {
            gausRifleProjectile.ResetProjectile();
            
            _weaponProjectilePool.ReleaseProjectile(gausRifleProjectile);
        }
    }
}