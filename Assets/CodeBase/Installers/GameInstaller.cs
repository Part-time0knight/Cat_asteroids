using Core.MVVM.Windows;
using Game.Domain.Factories.GameFsm;
using Game.Infrastructure;
using Game.Logic.Effects.Explosion;
using Game.Logic.Enemy;
using Game.Logic.Enemy.Asteroid.AsteroidB;
using Game.Logic.Enemy.Asteroid.AsteroidM;
using Game.Logic.Enemy.Asteroid.AsteroidS;
using Game.Logic.Enemy.Ice;
using Game.Logic.Enemy.Ice.IceM;
using Game.Logic.Enemy.Spawner;
using Game.Logic.Handlers;
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
            InstallServices();
            InstallDataObjects();
            InstallViewModel();
        }

        private void InstallFactory()
        {
            Container
                .BindInterfacesAndSelfTo<StatesFactory>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<EnemyHandler.Pool, ISpawner.Settings, ISpawner, SpawnerFactory>()
                .To<EnemySpawner>()
                .FromNew()
                .NonLazy();
        }

        private void InstallPools()
        {
            Container
                .BindMemoryPool<PlayerHandler, PlayerHandler.Pool>()
                .FromComponentInNewPrefab(_settings.PlayerPrefab);

            Container
                .BindMemoryPool<Bullet, Bullet.Pool>()
                .FromComponentInNewPrefab(_settings.Projectiles.BulletPrefab)
                .UnderTransform(_settings.Projectiles.Container);

            Container
                .BindMemoryPool<IceBullet, IceBullet.IcePool>()
                .FromComponentInNewPrefab(_settings.Projectiles.IceBulletPrefab)
                .UnderTransform(_settings.Projectiles.Container);

            Container
                .BindMemoryPool<EnemyHandler, EnemyHandler.Pool>()
                .WithId("AsteroidB")
                .FromComponentInNewPrefab(_settings.Enemies.AsteroidBigPrefab)
                .UnderTransform(_settings.Enemies.Container);

            Container
                .BindMemoryPool<EnemyHandler, EnemyHandler.Pool>()
                .WithId("AsteroidM")
                .FromComponentInNewPrefab(_settings.Enemies.AsteroidMediumPrefab)
                .UnderTransform(_settings.Enemies.Container);

            Container
                .BindMemoryPool<EnemyHandler, EnemyHandler.Pool>()
                .WithId("AsteroidS")
                .FromComponentInNewPrefab(_settings.Enemies.AsteroidSmallPrefab)
                .UnderTransform(_settings.Enemies.Container);

            Container
                .BindMemoryPool<EnemyHandler, EnemyHandler.Pool>()
                .WithId("IceM")
                .FromComponentInNewPrefab(_settings.Enemies.IceMediumPrefab)
                .UnderTransform(_settings.Enemies.Container);

            Container.BindMemoryPool<Explosion, Explosion.Pool>()
                .FromComponentInNewPrefab(_settings.Explosion.ExplosionPrefab)
                .UnderTransform(_settings.Explosion.Container);
        }

        private void InstallDataObjects()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerDynamicData>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallViewModel()
        {
            Container
                .BindInterfacesAndSelfTo<StartViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<GameplayViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<DefeatViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<PauseViewModel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<LoadViewModel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<ControlWindowViewModel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<SettingsWindowViewModel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<LeaderBoardViewModel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<PowerUpViewModel>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallServices()
        {
            Container.BindInstance(_settings.Explosion.BigExplosion).AsSingle();

            Container
                .BindInterfacesAndSelfTo<SceneLoadHandler>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<PauseInputHandler>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<EnemySpawnerService>()
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

            Container
                .BindInterfacesAndSelfTo<DifficultHandler>()
                .AsSingle()
                .NonLazy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField]
            public PlayerHandler PlayerPrefab { get; set; }

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

                [field: SerializeField]
                public Bullet IceBulletPrefab { get; private set; }
            }

            [Serializable]
            public class ExplosionSettings
            {
                [field: SerializeField]
                public Transform Container { get; private set; }

                [field: SerializeField]
                public Explosion ExplosionPrefab { get; private set; }

                [field: SerializeField]
                public ParticleSystem BigExplosion { get; private set; }
            }

            [Serializable]
            public class EnemySettings
            {
                [field: SerializeField]
                public Transform Container { get; private set; }

                [field: SerializeField]
                public AsteroidBigHandler AsteroidBigPrefab { get; private set; }

                [field: SerializeField]
                public AsteroidMediumHandler AsteroidMediumPrefab { get; private set; }

                [field: SerializeField]
                public AsteroidSmallHandler AsteroidSmallPrefab { get; private set; }

                [field: SerializeField]
                public IceHandler IceMediumPrefab { get; private set; }
            }
        }
    }
}