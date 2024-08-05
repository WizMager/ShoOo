﻿using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
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
            _weaponProjectilePool = new ProjectilePool<PistolBullet>(projectilePrefab, MagazineSize);
            _weaponProjectilePool.ProjectileInstantiated.Subscribe(OnProjectileInstantiated).AddTo(_disposable);

            CurrentProjectilesInMagazine = MagazineSize;
        }

        private void OnProjectileInstantiated(PistolBullet[] projectiles)
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

        //TODO: rework this crutch
        private async UniTaskVoid Reloading()
        {
            await UniTask.WaitForSeconds(ReloadTime);
            
            IsReloading = false;
            CurrentProjectilesInMagazine = MagazineSize;
        }
        
        private void OnExistProjectileEnded(PistolBullet pistolBullet)
        {
            pistolBullet.ResetProjectile();
            
            _weaponProjectilePool.ReleaseProjectile(pistolBullet);
        }
    }
}