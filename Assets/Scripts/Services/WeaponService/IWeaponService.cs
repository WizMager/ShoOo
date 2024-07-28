using UnityEngine;
using Utils.Weapons;

namespace Services.WeaponService
{
    public interface IWeaponService
    {
        AWeapon GetWeapon(EWeaponType weaponType);
        void ReleaseWeapon(AWeapon weapon);
    }
}