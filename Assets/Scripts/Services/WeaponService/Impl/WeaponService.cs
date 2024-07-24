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
            return _weaponPool.GetWeapon(weaponType);
        }

        public void ReleaseWeapon(Weapon weapon)
        {
            _weaponPool.ReleaseWeapon(weapon);
        }
    }
}