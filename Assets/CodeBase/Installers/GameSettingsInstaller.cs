using Game.Logic.Effects.Particles;
using Game.Logic.Enemy;
using Game.Logic.Enemy.Asteroid;
using Game.Logic.Misc;
using Game.Logic.Player;
using Game.Logic.Player.Animation;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [field: SerializeField] public PlayerSettings Player { get; private set; }
        [field: SerializeField] public ProjectileSettings Projectile { get; private set; }
        [field: SerializeField] public EnemySpawnerSettings SpawnerSettings { get; private set; }
        [field: SerializeField] public EnemiesSettings Enemies { get; private set; }


        [Serializable]
        public class PlayerSettings
        {
            public PlayerShootHandler.PlayerSettings Weapon;
            public PlayerMoveHandler.PlayerSettings Move;
            public PlayerDamageHandler.PlayerSettings Hits;
            public PlayerHasteEffectHandler.Settings Haste;
            public PlayerWeaponHandler.PlayerSettings Damage;
            public PlayerTakeDamage.Settings TakeDamageAnimation;
        }

        [Serializable]
        public class ProjectileSettings
        {
            public BulletMoveHandler.BulletSettngs Move;
        }

        [Serializable]
        public class EnemiesSettings
        {
            public AsteroidSettings Asteroid;

            [Serializable]
            public class AsteroidSettings
            {
                public EnemyMoveHandler.EnemySettings Move;
                public EnemyDamageHandler.EnemySettings Hits;
                public EnemyWeaponHandler.EnemySettings Damage;
                public AsteroidRotate.Settings Rotate;
                public AsteroidViewHandler.Settings View;
            }

        }

        [Serializable]
        public class EnemySpawnerSettings
        {
            public EnemySpawner.Settings Spawner;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Player.Weapon).AsSingle();
            Container.BindInstance(Player.Move).AsSingle();
            Container.BindInstance(Player.Hits).AsSingle();
            Container.BindInstance(Player.Haste).AsSingle();
            Container.BindInstance(Player.Damage).AsSingle();
            Container.BindInstance(Player.TakeDamageAnimation).AsSingle();

            Container.BindInstance(Projectile.Move).AsSingle();

            Container.BindInstance(SpawnerSettings.Spawner).AsSingle();

            Container.BindInstance(Enemies.Asteroid.Move).AsSingle();
            Container.BindInstance(Enemies.Asteroid.Hits).AsSingle();
            Container.BindInstance(Enemies.Asteroid.Damage).AsSingle();
            Container.BindInstance(Enemies.Asteroid.Rotate).AsSingle();
            Container.BindInstance(Enemies.Asteroid.View).AsSingle();
        }
    }
}