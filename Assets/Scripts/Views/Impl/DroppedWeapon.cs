using UnityEngine;
using Utils.Weapons;

namespace Views.Impl
{
    public class DroppedWeapon : AView, IDroppedWeapon
    {
        [field:SerializeField] public EWeaponType WeaponType { get; private set; }
        
        public void PickupWeapon()
        {
            Destroy(gameObject);//TODO: add pooling
        }
    }
}