using System;
using R3;
using Services.WeaponService;
using UnityEngine;
using Utils.Weapons;
using Zenject;

namespace Views.Modules.Impl
{
    public class AttackModule : AModule
    {
        [SerializeField] private Transform weaponHoldPoint;

        [Inject] private IWeaponService _weaponService;
        private Weapon _currentWeapon;

        public float FireRate => _currentWeapon.FireRate;
        
        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            base.Initialize(view, disposable);
            
            Observable.Timer(TimeSpan.FromSeconds(0.2)).Subscribe(_ => ChangeWeapon(EWeaponType.Pistol));
        }

        public void Shoot()
        {
            _currentWeapon.ShootProjectile();
        }
        
        public void ChangeWeapon(EWeaponType weaponType)
        {
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