using Utils.Weapons;

namespace Views
{
    public interface IDroppedWeapon
    {
        EWeaponType WeaponType { get; }
        void PickupWeapon();
    }
}