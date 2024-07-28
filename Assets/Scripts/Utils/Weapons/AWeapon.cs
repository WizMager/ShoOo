using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utils.Weapons
{
    public abstract class AWeapon : MonoBehaviour, IShootable
    {
        [SerializeField] protected AssetReference projectilePrefab;
        [SerializeField] protected Transform projectileShootPoint;
        [SerializeField] protected int damage;
        [SerializeField] protected float projectileSpeed;
        
        [field:SerializeField] public EWeaponType WeaponType { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; } = 2;

        public abstract void Initialize();
        public abstract void Shoot();
    }
}