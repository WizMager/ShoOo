using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Configs.Weapons
{
    public interface IWeaponBase
    {
        List<AssetReference> GetAllWeapons();
    }
}