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

        public AWeapon GetWeapon(EWeaponType weaponType)
        {
            var weapon = _weaponPool.GetWeapon(weaponType);
            
            return weapon;
        }

        public void ReleaseWeapon(AWeapon weapon)
        {
            weapon.gameObject.SetActive(false);
            
            _weaponPool.ReleaseWeapon(weapon);
        }
    }
}