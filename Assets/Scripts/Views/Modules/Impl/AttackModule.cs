using System;
using R3;
using Services.WeaponService;
using UnityEngine;
using Utils.Weapons;
using Views.Impl.Projectile.Impl;
using Zenject;

namespace Views.Modules.Impl
{
    public class AttackModule : AModule
    {
        [SerializeField] private Transform weaponHoldPoint;

        [Inject] private IWeaponService _weaponService;
        private IShootable _currentWeapon;

        public float FireRate => _currentWeapon.FireRate;
        
        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            base.Initialize(view, disposable);
            
            Observable.Timer(TimeSpan.FromSeconds(0.2)).Subscribe(_ => ChangeWeapon(EWeaponType.Pistol));
        }

        public void Shoot()
        {
            _currentWeapon.Shoot();
        }
        
        public void ChangeWeapon(EWeaponType weaponType)
        {
            _currentWeapon = _weaponService.GetWeapon(weaponType, weaponHoldPoint);
        }
    }
}