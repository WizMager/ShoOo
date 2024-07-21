using UnityEngine;
using Utils.ObjectPool;

namespace Utils.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private EWeaponType weaponType;
        [SerializeField] private int damage;
        [SerializeField] private float fireRate;
        
        private ProjectilePool _weaponProjectilePool;
    }
}