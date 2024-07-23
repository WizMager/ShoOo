using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs.Weapons.Impl
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(WeaponBase), fileName = nameof(WeaponBase))]
    public class WeaponBase : ScriptableObject, IWeaponBase
    {
        [SerializeField] private List<AssetReference> weapons;

        public List<AssetReference> GetAllWeapons() => weapons;
    }
}