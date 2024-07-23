using Configs.PrefabBase;
using Configs.PrefabBase.Impl;
using Configs.Weapons;
using Configs.Weapons.Impl;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = "Installer/" + nameof(GameConfigInstaller), fileName = nameof(GameConfigInstaller))]
    public class GameConfigInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private PrefabBase prefabBase;
        [SerializeField] private WeaponBase weaponBase;
        
        public override void InstallBindings()
        {
            Container.Bind<IPrefabBase>().FromInstance(prefabBase).AsSingle();
            Container.Bind<IWeaponBase>().FromInstance(weaponBase).AsSingle();
        }
    }
}