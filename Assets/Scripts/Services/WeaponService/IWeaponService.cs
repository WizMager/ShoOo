using Utils.Weapons;

namespace Services.WeaponService
{
    public interface IWeaponService
    {
        Weapon GetWeapon(EWeaponType weaponType);
        void ReleaseWeapon(Weapon weapon);
    }
}