using System;
using System.Collections.Generic;
using System.Linq;
using Configs.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.Weapons
{
    public class WeaponPool
    {
        private readonly List<AWeapon> _weaponList = new ();
        private readonly Transform _poolContainerTransform;
        
        public WeaponPool(IWeaponBase weaponBase)
        {
            var poolContainer = new GameObject("WeaponPoolContainer");
            _poolContainerTransform = poolContainer.transform;
            
            InstantiateProjectilesAtStart(weaponBase);
        }
        
        public AWeapon GetWeapon(EWeaponType weaponType)
        {
            foreach (var weapon in _weaponList.Where(weapon => weapon.WeaponType == weaponType))
            {
                weapon.gameObject.SetActive(true);
                
                return weapon;
            }

            throw new Exception($"[{typeof(WeaponPool)}]; There is no weapon with type {weaponType} in list.");
        }

        public void ReleaseWeapon(AWeapon weapon)
        {
            weapon.transform.SetParent(_poolContainerTransform);
            weapon.gameObject.SetActive(false);
            
            _weaponList.Add(weapon);
        }

        private void InstantiateProjectilesAtStart(IWeaponBase weaponBase)
        {
            foreach (var weapon in weaponBase.GetAllWeapons())
            {
                var instantiatedFirstCopyWeapon = Object.Instantiate(weapon);
                var instantiatedSecondCopyWeapon = Object.Instantiate(weapon);
                var firstWeapon = instantiatedFirstCopyWeapon.GetComponent<AWeapon>();
                var secondWeapon = instantiatedSecondCopyWeapon.GetComponent<AWeapon>();
                
                SetupWeapon(firstWeapon);
                SetupWeapon(secondWeapon);
            }
            
            return;

            void SetupWeapon(AWeapon weapon)
            {
                weapon.Initialize();
                weapon.transform.SetParent(_poolContainerTransform);
                weapon.gameObject.SetActive(false);
                
                _weaponList.Add(weapon);
            }
        }
    }
}