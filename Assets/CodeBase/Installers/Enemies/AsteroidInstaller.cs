using Game.Logic.Enemy.Asteroid;
using System;
using UnityEngine;

namespace Installers.Enemies
{
    public class AsteroidInstaller : EnemyInstaller
    {
        [SerializeField] private AsteroidSettings _asteroidSettings;

        protected override void InstallEnemyComponents()
        {
            base.InstallEnemyComponents();
            Container
                .BindInstance(_asteroidSettings.Sprite)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidRotate>().AsSingle();
        }

        [Serializable]
        public class AsteroidSettings
        {
            [field: SerializeField] public SpriteRenderer Sprite { get; private set; }
        }
    }
}