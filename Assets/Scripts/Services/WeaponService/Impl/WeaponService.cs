using Configs.Weapons;
using UnityEngine;
using Utils.Weapons;

namespace Services.WeaponService.Impl
{
    public class WeaponService : IWeaponService
    {
        private readonly WeaponPool _weaponPool;

        public WeaponService(IWeaponBase weaponBase)
        {
            _weaponPool = new WeaponPool(weaponBase);
        }

        public AWeapon GetWeapon(EWeaponType weaponType, Transform weaponHoldPoint)
        {
            var weapon = _weaponPool.GetWeapon(weaponType);
            weapon.gameObject.SetActive(true);
            
            return weapon;
        }

        public void ReleaseWeapon(AWeapon aWeapon)
        {
            aWeapon.gameObject.SetActive(false);
            _weaponPool.ReleaseWeapon(aWeapon);
        }
    }
}