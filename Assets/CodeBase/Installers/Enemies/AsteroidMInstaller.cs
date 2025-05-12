using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy;
using Game.Logic.Enemy.Asteroid;
using Game.Logic.Enemy.Asteroid.AsteroidM;
using Game.Logic.Enemy.Asteroid.Fsm;
using Game.Logic.Enemy.Fsm;
using System;
using UnityEngine;
using Zenject;

namespace Installers.Enemies
{
    public class AsteroidMInstaller : EnemyInstaller
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
                .To<AsteroidMRotate>()
                .AsSingle();
            Container
                .Bind<AsteroidViewHandler>()
                .To<AsteroidMViewHandler>()
                .AsSingle();

            Container
                .Bind<EnemyWeaponHandler>()
                .To<AsteroidMWeaponHandler>()
                .AsSingle();
            Container
                .Bind<EnemyMoveHandler>()
                .To<AsteroidMMoveHandler>()
                .AsSingle();
            Container
                .Bind<EnemyDamageHandler>()
                .To<AsteroidMDamageHandler>()
                .AsSingle();
        }

        [Serializable]
        public class AsteroidSettings
        {
            [field: SerializeField] public SpriteRenderer Sprite { get; private set; }
        }
    }
}