using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Utils.ObjectPool;
using Views.Impl.Projectile.Impl;
using Random = UnityEngine.Random;

namespace Utils.Weapons.Impl
{
    public class ShotgunWeapon : AWeapon
    {
        [SerializeField] private float shotSpreadAngle;
        [SerializeField] private int bulletsInShot;
        
        private ProjectilePool<ShotgunBullet> _weaponProjectilePool;
        
        private readonly CompositeDisposable _disposable = new ();
        
        public override void Initialize()
        {
            _weaponProjectilePool = new ProjectilePool<ShotgunBullet>(projectilePrefab, MagazineSize, bulletsInShot);
            
            _weaponProjectilePool.ProjectileInstantiated.Subscribe(OnProjectileInstantiated).AddTo(_disposable);

            CurrentProjectilesInMagazine = MagazineSize;
        }

        private void OnProjectileInstantiated(ShotgunBullet[] projectiles)
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
            
            for (var i = 0; i < bulletsInShot; i++)
            {
                var direction = CalculateProjectileDirection();
                
                ShotSingleProjectile(direction);
            }
        }

        private Vector3 CalculateProjectileDirection()
        {
            var forwardDirection = projectileShootPoint.forward;
            var shotAngle = Random.Range(0, shotSpreadAngle / 2);
            
            var spreadAngleRotate = Random.Range(0, 2);
            
            if (spreadAngleRotate == 1)
            {
                shotAngle = -shotAngle;
            }
            
            return Quaternion.AngleAxis(shotAngle, transform.up) * forwardDirection;
        }
        
        private void ShotSingleProjectile(Vector3 shotDirection)
        {
            var (isNewProjectile, projectileView) = _weaponProjectilePool.GetProjectile();
            
            if (isNewProjectile)
            {
                projectileView.ExistProjectileEnded.Subscribe(_ => OnExistProjectileEnded(projectileView)).AddTo(_disposable);
            }

            projectileView.transform.position = projectileShootPoint.position;
            projectileView.transform.rotation = projectileShootPoint.rotation;
            
            projectileView.ActivateProjectile(Damage);
            projectileView.Fly(ProjectileSpeed, shotDirection);
        }
        
        //TODO: rework this crutch
        private async UniTaskVoid Reloading()
        {
            await UniTask.WaitForSeconds(ReloadTime);

            IsReloading = false;
            CurrentProjectilesInMagazine = MagazineSize;
        }
        
        private void OnExistProjectileEnded(ShotgunBullet shotgunBullet)
        {
            shotgunBullet.ResetProjectile();
            _weaponProjectilePool.ReleaseProjectile(shotgunBullet);
        }
    }
}