using UnityEngine;
using Utils.Weapons;

namespace Services.WeaponService
{
    public interface IWeaponService
    {
        AWeapon GetWeapon(EWeaponType weaponType, Transform weaponHoldPoint);
        void ReleaseWeapon(AWeapon aWeapon);
    }
}