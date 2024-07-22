using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.ObjectPool;
using Views.Impl;

namespace Utils.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float fireRate;
        [SerializeField] private AssetReference projectilePrefab;
        [SerializeField] private bool isOneTouchProjectiles;
        [SerializeField] private float projectileSpeed;
        
        private ProjectilePool _weaponProjectilePool;
        
        [field:SerializeField] public EWeaponType WeaponType { get; private set; }

        public void Initialize()
        {
            _weaponProjectilePool = new ProjectilePool(projectilePrefab);
        }
        
        public ProjectileView ShootProjectile(Vector3 shootPosition, Quaternion shootRotation)
        {
            var projectile = _weaponProjectilePool.GetProjectile();

            projectile.transform.position = shootPosition;
            projectile.transform.rotation = shootRotation;
            
            projectile.ActivateProjectile();

            return projectile;
        }
    }
}