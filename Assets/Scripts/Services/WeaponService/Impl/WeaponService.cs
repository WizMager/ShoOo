using Configs.Weapons;
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

        public Weapon GetWeapon(EWeaponType weaponType)
        {
            var weapon = _weaponPool.GetWeapon(weaponType);
            weapon.gameObject.SetActive(true);
            
            return weapon;
        }

        public void ReleaseWeapon(Weapon weapon)
        {
            weapon.gameObject.SetActive(false);
            _weaponPool.ReleaseWeapon(weapon);
        }
    }
}