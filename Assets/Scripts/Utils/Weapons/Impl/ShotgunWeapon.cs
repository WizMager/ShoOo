using R3;
using UnityEngine;
using Utils.ObjectPool;
using Views.Impl.Projectile.Impl;

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
            _weaponProjectilePool = new ProjectilePool<ShotgunBullet>(projectilePrefab);
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
            var forwardDirection = transform.forward;
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
            var projectile = _weaponProjectilePool.GetProjectile();
            
            projectile.transform.position = projectileShootPoint.position;
            projectile.transform.rotation = projectileShootPoint.rotation;
            
            projectile.ActivateProjectile(damage);
            projectile.Fly(projectileSpeed, shotDirection);
            
            projectile.ProjectileTouchedTarget.Subscribe(_ => OnProjectileTouchedTarget(projectile)).AddTo(_disposable);
        }
        
        private void OnProjectileTouchedTarget(ShotgunBullet shotgunBullet)
        {
            _weaponProjectilePool.ReleaseProjectile(shotgunBullet);
        }
    }
}