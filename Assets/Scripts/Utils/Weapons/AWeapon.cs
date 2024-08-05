﻿using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utils.Weapons
{
    public abstract class AWeapon : MonoBehaviour, IShootable
    {
        [SerializeField] protected AssetReference projectilePrefab;
        [SerializeField] protected Transform projectileShootPoint;
        
        protected int CurrentProjectilesInMagazine;
        protected bool IsReloading;
        
        [field:SerializeField] public int Damage { get; private set; }
        [field:SerializeField] public float ProjectileSpeed { get; private set; }
        [field:SerializeField] public EWeaponType WeaponType { get; private set; }
        [field:SerializeField] public float FireRate { get; private set; }
        [field:SerializeField] public int MagazineSize{ get; private set; }
        [field:SerializeField] public float ReloadTime{ get; private set; }
        
        public abstract void Initialize();
        public abstract void Shoot();
    }
}