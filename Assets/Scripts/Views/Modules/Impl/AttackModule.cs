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
        [SerializeField] private Transform shootPoint;

        [Inject] private IWeaponService _weaponService;
        private Weapon _currentWeapon;
        
        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            base.Initialize(view, disposable);
            
            Observable.Timer(TimeSpan.FromSeconds(0.2)).Subscribe(_ => ChangeWeapon(EWeaponType.Pistol));
        }

        public void Shoot()
        {
            _currentWeapon.ShootProjectile(shootPoint.position, shootPoint.rotation);
        }
        
        public void ChangeWeapon(EWeaponType weaponType)
        {
            _currentWeapon = _weaponService.GetWeapon(weaponType);
            _currentWeapon.transform.SetParent(shootPoint);
            _currentWeapon.transform.position = shootPoint.position;
            _currentWeapon.transform.rotation = shootPoint.rotation;
        }
    }
}