using System.Collections.Generic;
using UnityEngine;

namespace Configs.Weapons
{
    public interface IWeaponBase
    {
        List<GameObject> GetAllWeapons();
    }
}