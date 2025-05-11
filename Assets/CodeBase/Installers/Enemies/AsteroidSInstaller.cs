using Game.Logic.Enemy.Asteroid.AsteroidS;
using Game.Logic.Enemy.Asteroid;
using Game.Logic.Enemy;
using System;
using UnityEngine;

namespace Installers.Enemies
{
    public class AsteroidSInstaller : EnemyInstaller
    {
        [SerializeField] private AsteroidSettings _asteroidSettings;

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
                .To<AsteroidSRotate>()
                .AsSingle();
            Container
                .Bind<AsteroidViewHandler>()
                .To<AsteroidSViewHandler>()
                .AsSingle();

            Container
                .Bind<EnemyWeaponHandler>()
                .To<AsteroidSWeaponHandler>()
                .AsSingle();
            Container
                .Bind<EnemyMoveHandler>()
                .To<AsteroidSMoveHandler>()
                .AsSingle();
            Container
                .Bind<EnemyDamageHandler>()
                .To<AsteroidSDamageHandler>()
                .AsSingle();
        }

        [Serializable]
        public class AsteroidSettings
        {
            [field: SerializeField] public SpriteRenderer Sprite { get; private set; }
        }
    }
}