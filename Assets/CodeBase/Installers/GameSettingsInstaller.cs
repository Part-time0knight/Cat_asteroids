using Game.Logic.Enemy;
using Game.Logic.Misc;
using Game.Logic.Player;
using System;
using System.ComponentModel;
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
        [field: SerializeField] public EnemySettings Enemy { get; private set; }


        [Serializable]
        public class PlayerSettings
        {
            public PlayerShootHandler.PlayerSettings Weapon;
            public PlayerMoveHandler.PlayerSettings Move;
            public PlayerDamageHandler.PlayerSettings Hits;
        }

        [Serializable]
        public class ProjectileSettings
        {
            public BulletMoveHandler.BulletSettngs Move;
        }

        [Serializable]
        public class EnemySettings
        {
            public EnemyMoveHandler.EnemySettings Move;
            public EnemyDamageHandler.EnemySettings Hits;
            public EnemyWeaponHandler.Settings Damage;
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

            Container.BindInstance(Projectile.Move).AsSingle();

            Container.BindInstance(SpawnerSettings.Spawner).AsSingle();

            Container.BindInstance(Enemy.Move).AsSingle();
            Container.BindInstance(Enemy.Hits).AsSingle();
            Container.BindInstance(Enemy.Damage).AsSingle();
        }
    }
}