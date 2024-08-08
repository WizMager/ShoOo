using R3;
using R3.Triggers;
using UnityEngine;
using Utils.LayerMask;
using Views.Modules.Impl;

namespace Views.Impl
{
    public class PlayerCharacterView : AView
    {
        [SerializeField] private Collider _viewCollider;

        public override void Initialize()
        {
            base.Initialize();

            _viewCollider.OnTriggerEnterAsObservable().Subscribe(OnTriggerEntered).AddTo(Disposable);
        }

        private void OnTriggerEntered(Collider other)
        {
            //if ((other.gameObject.layer & Layers.DroppedWeaponLayer) == 0) return;
            if (LayerMasks.DroppedWeapon != (LayerMasks.DroppedWeapon | (1 << other.gameObject.layer))) 
                return;
            
            if (!other.TryGetComponent<IDroppedWeapon>(out var droppedWeapon)) 
                return;
            
            var pickedUpWeapon = droppedWeapon.WeaponType;
            
            var weaponModule = GetModule<WeaponModule>();
            weaponModule.ChangeWeapon(pickedUpWeapon);
                    
            droppedWeapon.PickupWeapon();
        }
    }
}