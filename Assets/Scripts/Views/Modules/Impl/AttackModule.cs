using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Utils.Weapons;

namespace Views.Modules.Impl
{
    public class AttackModule : AModule
    {
        [SerializeField] private List<Weapon> weapons;

        private EWeaponType _currentWeapon = EWeaponType.Pistol; 
        
        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            base.Initialize(view, disposable);
        }

        public void Shoot()
        {
            
        }
    }
}