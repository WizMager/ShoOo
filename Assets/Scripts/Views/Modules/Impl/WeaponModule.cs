using System;
using R3;
using Services.WeaponService;
using UnityEngine;
using Utils.Weapons;
using Zenject;

namespace Views.Modules.Impl
{
    public class WeaponModule : AModule
    {
        [SerializeField] private Transform weaponHoldPoint;

        [Inject] private IWeaponService _weaponService;
        private AWeapon _currentWeapon;

        public float FireRate => _currentWeapon.FireRate;
        
        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            base.Initialize(view, disposable);
            
            Observable.Timer(TimeSpan.FromSeconds(0.2)).Subscribe(_ => ChangeWeapon(EWeaponType.Pistol));//TODO: remove timer, initialize pools another way
        }

        public void Shoot()
        {
            _currentWeapon.Shoot();
        }

        public void Reload()
        {
            _currentWeapon.Reload();
        }
        
        public void ChangeWeapon(EWeaponType weaponType)
        {
            if (_currentWeapon != null)
            {
                _weaponService.ReleaseWeapon(_currentWeapon);
            }
            
            _currentWeapon = _weaponService.GetWeapon(weaponType);
            
            SetupWeapon();
        }
        
        private void SetupWeapon()
        {
            _currentWeapon.transform.SetParent(weaponHoldPoint);
            _currentWeapon.transform.position = weaponHoldPoint.position;
            _currentWeapon.transform.rotation = weaponHoldPoint.rotation;
        }
    }
}