using Core.MVVM.Windows;
using Game.Domain.Factories.GameFsm;
using Game.Infrastructure;
using Game.Logic.Enemy;
using Game.Logic.Handlers;
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
            Container.Bind<BulletBuffer>().FromComponentInNewPrefab(_settings.BufferPrefab).AsSingle();
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .FromComponentInNewPrefab(_settings.BulletPrefab);

            //Container.BindMemoryPool<EnemyHandler, EnemyHandler.Pool>()
            //    .FromComponentInNewPrefab(_settings.EnemyHandler);
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

            //Container
            //    .BindInterfacesAndSelfTo<EnemySpawner>()
            //    .AsSingle()
            //    .NonLazy();

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
            public BulletBuffer BufferPrefab { get; private set; }

            [field: SerializeField]
            public Bullet BulletPrefab { get; private set; }

            //[field: SerializeField]
            //public EnemyHandler EnemyHandler { get; private set; }
        }
    }
}