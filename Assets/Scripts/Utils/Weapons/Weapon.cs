using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.ObjectPool;
using Views.Impl.Projectile.Impl;

namespace Utils.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private AssetReference projectilePrefab;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private Transform projectileShootPoint;
        
        private ProjectilePool<PistolBullet> _weaponProjectilePool;
        
        [field:SerializeField] public EWeaponType WeaponType { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; } = 2;

        public void Initialize()
        {
            _weaponProjectilePool = new ProjectilePool<PistolBullet>(projectilePrefab);
        }
        
        public PistolBullet ShootProjectile()
        {
            var projectile = _weaponProjectilePool.GetProjectile();

            projectile.transform.position = projectileShootPoint.position;
            projectile.transform.rotation = projectileShootPoint.rotation;
            
            projectile.ActivateProjectile();
            projectile.Fly(projectileSpeed);

            return projectile;
        }
    }
}