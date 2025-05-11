using Game.Logic.Enemy.Asteroid.AsteroidB;
using Game.Logic.Enemy.Asteroid;
using Game.Logic.Enemy;
using System;
using UnityEngine;

namespace Installers.Enemies
{
    public class AsteroidBInstaller : EnemyInstaller
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
        }

        [Serializable]
        public class AsteroidSettings
        {
            [field: SerializeField] public SpriteRenderer Sprite { get; private set; }
        }
    }
}