using Core.MVVM.Windows;
using Game.Domain.Factories.GameFsm;
using Game.Infrastructure;
using Game.Logic.Effects.Explosion;
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
            InstallViewModel();

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
            Container
                .BindMemoryPool<Bullet, Bullet.Pool>()
                .FromComponentInNewPrefab(_settings.Projectiles.BulletPrefab)
                .UnderTransform(_settings.Projectiles.Container);

            
            Container
                .BindMemoryPool<EnemyHandler, EnemyHandler.Pool>()
                .FromComponentInNewPrefab(_settings.Enemies.EnemyHandlerPrefab)
                .UnderTransform(_settings.Enemies.Container);

            Container.BindMemoryPool<Explosion, Explosion.Pool>()
                .FromComponentInNewPrefab(_settings.Explosion.ExplosionPrefab)
                .UnderTransform(_settings.Explosion.Container);
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
            //Container
            //    .BindInterfacesAndSelfTo<TestingToolsViewModel>()
            //    .AsSingle()
            //    .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameplayViewModel>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallService()
        {
            Container
                .BindInterfacesAndSelfTo<EnemySpawner>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<ExplosionSpawner>()
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
            [field: SerializeField]
            public ExplosionSettings Explosion { get; private set; }

            [Serializable]
            public class ProjectileSettings
            {
                [field: SerializeField]
                public Transform Container { get; private set; }

                [field: SerializeField]
                public Bullet BulletPrefab { get; private set; }
            }

            [Serializable]
            public class ExplosionSettings
            {
                [field: SerializeField]
                public Transform Container { get; private set; }

                [field: SerializeField]
                public Explosion ExplosionPrefab { get; private set; }

            }

            [Serializable]
            public class EnemySettings
            {
                [field: SerializeField]
                public EnemyHandler EnemyHandlerPrefab { get; private set; }

                [field: SerializeField]
                public Transform Container { get; private set; }
            }
        }
    }
}