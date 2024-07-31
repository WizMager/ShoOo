using System.Collections.Generic;
using UnityEngine;

namespace Configs.Weapons.Impl
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(WeaponBase), fileName = nameof(WeaponBase))]
    public class WeaponBase : ScriptableObject, IWeaponBase
    {
        [SerializeField] private List<GameObject> weapons;

        public List<GameObject> GetAllWeapons() => weapons;
    }
}