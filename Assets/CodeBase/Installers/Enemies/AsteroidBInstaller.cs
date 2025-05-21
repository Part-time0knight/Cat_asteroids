using Game.Logic.Enemy.Asteroid.AsteroidB;
using Game.Logic.Enemy.Asteroid;
using Game.Logic.Enemy;
using System;
using UnityEngine;
using Game.Logic.Enemy.Asteroid.Fsm;
using Game.Logic.Enemy.Fsm;
using Zenject;
using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy.Ice;

namespace Installers.Enemies
{
    public class AsteroidBInstaller : EnemyInstaller
    {
        [SerializeField] private AsteroidSettings _asteroidSettings;

        protected override void InstallFsm()
        {
            Container
                .Bind<EnemyFsm>()
                .To<AsteroidFsm>()
                .AsSingle()
                .NonLazy();
            Container.Bind<IInitializable>().FromResolveGetter<EnemyFsm>(fsm => fsm);
            Container.Bind<IGameStateMachine>().FromResolveGetter<EnemyFsm>(fsm => fsm);
        }

        protected override void InstallEnemyComponents()
        {

            Container
                .BindInstance(_asteroidSettings.Sprite)
                .AsSingle();

            Container
                .BindInstance(_settings.Body)
                .AsSingle();

            Container
                .Bind<AsteroidRotate>()
                .To<AsteroidBRotate>()
                .AsSingle();
            Container
                .Bind<AsteroidViewHandler>()
                .To<AsteroidBViewHandler>()
                .AsSingle();

            Container
                .Bind<EnemyWeaponHandler>()
                .To<AsteroidBWeaponHandler>()
                .AsSingle();
            Container
                .Bind<EnemyMoveHandler>()
                .To<AsteroidBMoveHandler>()
                .AsSingle();
            Container
                .Bind<EnemyDamageHandler>()
                .To<AsteroidBDamageHandler>()
                .AsSingle();

            Container
                .Bind<IDisposable>()
                .FromResolveGetter<AsteroidRotate>(fsm => fsm);
        }

        [Serializable]
        public class AsteroidSettings
        {
            [field: SerializeField] public SpriteRenderer Sprite { get; private set; }
        }
    }
}