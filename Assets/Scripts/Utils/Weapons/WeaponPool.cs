using System;
using System.Collections.Generic;
using System.Linq;
using Configs.Weapons;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
            
            InstantiateProjectilesAtStart(weaponBase).Forget();
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

        private async UniTaskVoid InstantiateProjectilesAtStart(IWeaponBase weaponBase)
        {
            var asyncOperations = new List<AsyncOperationHandle<GameObject>>();

            foreach (var weapon in weaponBase.GetAllWeapons())
            {
                var instantiateAsync = Addressables.InstantiateAsync(weapon);
                var instantiateSecondAsync = Addressables.InstantiateAsync(weapon);
                
                asyncOperations.Add(instantiateAsync);
                asyncOperations.Add(instantiateSecondAsync);
            }
            
            await UniTask.WhenAll(asyncOperations.Select(o => o.Task.AsUniTask()).ToArray());
            
            foreach (var asyncOperationHandle in asyncOperations)
            {
                var hasWeaponComponent = asyncOperationHandle.Result.TryGetComponent(out AWeapon weapon);
                
                if (!hasWeaponComponent)
                {
                    throw new Exception($"[{nameof(WeaponPool)}]: There is no {nameof(AWeapon)} component on {asyncOperationHandle.Result.name}");
                }
                
                weapon.Initialize();
                weapon.transform.SetParent(_poolContainerTransform);
                weapon.gameObject.SetActive(false);
                
                _weaponList.Add(weapon);
            }
        }
    }
}