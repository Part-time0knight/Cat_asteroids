using Game.Logic.Enemy;
using Game.Logic.Enemy.Fsm;
using System;
using UnityEngine;
using Zenject;

namespace Installers.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] protected Settings _settings;

        public override void InstallBindings()
        {
            InstallFactories();
            InstallFsm();

            InstallEnemyComponents();

        }

        protected virtual void InstallEnemyComponents()
        {
            Container
                .BindInstance(_settings.Body)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EnemyWeaponHandler>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<EnemyMoveHandler>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<EnemyTickHandler>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<EnemyDamageHandler>()
                .AsSingle();
        }

        protected virtual void InstallFsm()
        {
            Container
                .BindInterfacesTo<EnemyWindowFsm>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<EnemyFsm>()
                .AsSingle()
                .NonLazy();
        }

        protected virtual void InstallFactories()
        {
            Container
                .BindInterfacesAndSelfTo<EnemyStatesFactory>()
                .AsSingle()
                .NonLazy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Rigidbody2D Body { get; private set; }
        }
    }
}