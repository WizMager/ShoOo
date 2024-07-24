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
        private readonly List<Weapon> _weaponList = new ();
        private readonly Transform _poolContainerTransform;
        
        public WeaponPool(IWeaponBase weaponBase)
        {
            var poolContainer = new GameObject("WeaponPoolContainer");
            _poolContainerTransform = poolContainer.transform;
            
            InstantiateProjectilesAtStart(weaponBase).Forget();
        }
        
        public Weapon GetWeapon(EWeaponType weaponType)
        {
            foreach (var weapon in _weaponList.Where(weapon => weapon.WeaponType == weaponType))
            {
                return weapon;
            }

            throw new Exception($"[{typeof(WeaponPool)}]; There is no weapon with type {weaponType} in list.");
        }

        public void ReleaseWeapon(Weapon weapon)
        {
            weapon.transform.SetParent(_poolContainerTransform);
            
            _weaponList.Add(weapon);
        }

        private async UniTaskVoid InstantiateProjectilesAtStart(IWeaponBase weaponBase)
        {
            var asyncOperations = new List<AsyncOperationHandle<GameObject>>();

            foreach (var weapon in weaponBase.GetAllWeapons())
            {
                var instantiateAsync = Addressables.InstantiateAsync(weapon);
                Debug.Log(instantiateAsync.Status);
                
                asyncOperations.Add(instantiateAsync);
            }
            
            await UniTask.WhenAll(asyncOperations.Select(o => o.Task.AsUniTask()).ToArray());
            Debug.Log(asyncOperations.Count);
            foreach (var asyncOperationHandle in asyncOperations)
            {
                var hasWeaponComponent = asyncOperationHandle.Result.TryGetComponent(out Weapon weapon);
                
                if (!hasWeaponComponent)
                {
                    throw new Exception($"[{nameof(WeaponPool)}]: There is no {nameof(Weapon)} component on {asyncOperationHandle.Result.name}");
                }
                
                weapon.transform.SetParent(_poolContainerTransform);
                
                _weaponList.Add(weapon);
            }
        }
    }
}