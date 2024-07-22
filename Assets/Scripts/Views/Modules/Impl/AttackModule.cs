using System.Collections.Generic;
using R3;
using UnityEngine;
using Utils.Weapons;

namespace Views.Modules.Impl
{
    public class AttackModule : AModule
    {
        [SerializeField] private List<Weapon> weapons;
        [SerializeField] private Transform shootPoint;

        private Weapon _currentWeapon; 
        
        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            foreach (var weapon in weapons)
            {
                weapon.Initialize();
            }
            
            base.Initialize(view, disposable);
            
            ChangeWeapon(EWeaponType.Pistol);
        }

        public void Shoot()
        {
            _currentWeapon.ShootProjectile(shootPoint.position, shootPoint.rotation);
        }
        
        public void ChangeWeapon(EWeaponType weaponType)
        {
            foreach (var weapon in weapons)
            {
                if (weaponType != weapon.WeaponType) continue;

                _currentWeapon = weapon;
                return;
            }
            
            Debug.LogError($"[{typeof(AttackModule)}]: There is no weapon with type {weaponType}");
        }
    }
}