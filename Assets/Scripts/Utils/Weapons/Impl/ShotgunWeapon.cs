using System;
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
            _weaponProjectilePool = new ProjectilePool<ShotgunBullet>(projectilePrefab, bulletsInShot);
            
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
            
            projectileView.ActivateProjectile(damage);
            projectileView.Fly(projectileSpeed, shotDirection);
        }
        
        private void OnExistProjectileEnded(ShotgunBullet shotgunBullet)
        {
            _weaponProjectilePool.ReleaseProjectile(shotgunBullet);
        }
    }
}