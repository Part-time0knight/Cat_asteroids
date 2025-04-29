using Core.MVVM.Windows;
using Game.Domain.Factories.GameFsm;
using Game.Infrastructure;
using Game.Logic.Enemy;
using Game.Logic.Player;
using Game.Logic.Weapon;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallFactory();
            InstallPools();
            InstallService();
            InstallDataObjects();
            //InstallViewModel();

        }

        private void InstallFactory()
        {
            Container
                .BindInterfacesAndSelfTo<StatesFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallPools()
        {
            Container.Bind<BulletBuffer>().FromComponentInNewPrefab(_settings.Projectiles.BufferPrefab).AsSingle();
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .FromComponentInNewPrefab(_settings.Projectiles.BulletPrefab);

            Container.Bind<EnemyBuffer>().FromComponentInNewPrefab(_settings.Enemies.BufferPrefab).AsSingle();
            Container.BindMemoryPool<EnemyHandler, EnemyHandler.Pool>()
                .FromComponentInNewPrefab(_settings.Enemies.EnemyHandlerPrefab);
        }

        private void InstallDataObjects()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerData>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallViewModel()
        {
            Container
                .BindInterfacesAndSelfTo<TestingToolsViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<GameplayButtonsViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<MenuPauseViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<SettingsViewModel>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallService()
        {
            //Container
            //    .BindInterfacesTo<PauseHandler>()
            //    .AsSingle()
            //    .NonLazy();

            Container
                .BindInterfacesAndSelfTo<EnemySpawner>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<WindowFsm>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameFsm>()
                .AsSingle()
                .NonLazy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField]
            public ProjectileSettings Projectiles { get; private set; }

            [field: SerializeField]
            public EnemySettings Enemies { get; private set; }

            [Serializable]
            public class ProjectileSettings
            {
                [field: SerializeField]
                public BulletBuffer BufferPrefab { get; private set; }

                [field: SerializeField]
                public Bullet BulletPrefab { get; private set; }
            }

            [Serializable]
            public class EnemySettings
            {
                [field: SerializeField]
                public EnemyHandler EnemyHandlerPrefab { get; private set; }

                [field: SerializeField]
                public EnemyBuffer BufferPrefab { get; private set; }
            }
        }
    }
}