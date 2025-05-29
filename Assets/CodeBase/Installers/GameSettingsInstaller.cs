using Game.Logic.Effects.Particles;
using Game.Logic.Enemy.Asteroid.AsteroidB;
using Game.Logic.Enemy.Asteroid.AsteroidM;
using Game.Logic.Enemy.Asteroid.AsteroidS;
using Game.Logic.Enemy.Ice;
using Game.Logic.Enemy.Ice.IceM;
using Game.Logic.Enemy.Spawner;
using Game.Logic.Handlers;
using Game.Logic.Misc;
using Game.Logic.Player;
using Game.Logic.Player.Animation;
using Game.Logic.Player.Handlers;
using Game.Logic.Projectiles;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [field: SerializeField] public PlayerSettings Player { get; private set; }
        [field: SerializeField] public Gameplay GameplayParams { get; private set; }
        [field: SerializeField] public ProjectilesSettings Projectile { get; private set; }
        [field: SerializeField] public EnemySpawnerSettings SpawnerSettings { get; private set; }
        [field: SerializeField] public EnemiesSettings Enemies { get; private set; }

        [Serializable]
        public class Gameplay
        {
            public DifficultHandler.Settings Difficult;
        }

        [Serializable]
        public class PlayerSettings
        {
            public PlayerFacade.PlayerSettings MainSettings;
            public PlayerBaseShootHandler.PlayerSettings Weapon;
            public PlayerBaseMoveHandler.PlayerSettings Move;
            public PlayerDamageHandler.PlayerSettings Hits;
            public PlayerHasteEffect.Settings Haste;
            public PlayerWeaponHandler.PlayerSettings Damage;
            public PlayerTakeDamage.Settings TakeDamageAnimation;
            public PlayerInvincibilityHandler.Settings Invincibility;
        }

        [Serializable]
        public class ProjectilesSettings
        {
            public BulletMoveHandler.BulletSettings BaseMove;

            public IceBulletMoveHandler.IceBulletSettings IceMove;

            public Laser.Settings Laser;
        }


        [Serializable]
        public class EnemiesSettings
        {
            public AsteroidBigSettings AsteroidBig;
            public AsteroidMediumSettings AsteroidMedium;
            public AsteroidSmallSettings AsteroidSmall;
            public IceMSettings IceMedium;

            [Serializable]
            public class AsteroidSmallSettings
            {
                public AsteroidSmallHandler.AsteroidSettings MainSettings;
                public AsteroidSMoveHandler.AsteroidSettings Move;
                public AsteroidSDamageHandler.AsteroidSettings Hits;
                public AsteroidSWeaponHandler.AsteroidSettings Damage;
                public AsteroidSRotate.AsteroidSSettings Rotate;
                public AsteroidSViewHandler.AsteroidSSettings View;
            }

            [Serializable]
            public class AsteroidMediumSettings
            {
                public AsteroidMediumHandler.AsteroidSettings MainSettings;
                public AsteroidMMoveHandler.AsteroidSettings Move;
                public AsteroidMDamageHandler.AsteroidSettings Hits;
                public AsteroidMWeaponHandler.AsteroidSettings Damage;
                public AsteroidMRotate.AsteroidMSettings Rotate;
                public AsteroidMViewHandler.AsteroidMSettings View;
            }

            [Serializable]
            public class AsteroidBigSettings
            {
                public AsteroidBigHandler.AsteroidSettings MainSettings;
                public AsteroidBMoveHandler.AsteroidSettings Move;
                public AsteroidBDamageHandler.AsteroidSettings Hits;
                public AsteroidBWeaponHandler.AsteroidSettings Damage;
                public AsteroidBRotate.AsteroidBSettings Rotate;
                public AsteroidBViewHandler.AsteroidBSettings View;
            }

            [Serializable]
            public class IceMSettings
            {
                public IceHandler.IceSettings MainSettings;
                public IceMMoveHandler.IceMSettings Move;
                public IceMDamageHandler.IceSettings Hits;
                public IceMWeaponHandler.IceSettings Damage;
                public IceMShootHandler.IceMSettings Shooter;
            }
        }

        [Serializable]
        public class EnemySpawnerSettings
        {
            public ISpawner.Settings AsteroidBSpawner;
            public ISpawner.Settings AsteroidMSpawner;
            public ISpawner.Settings AsteroidSSpawner;
            public ISpawner.Settings IceMSpawner;
        }

        public override void InstallBindings()
        {
            InstallPlayer();

            Container.BindInstance(Projectile.BaseMove).AsSingle();
            Container.BindInstance(Projectile.Laser).AsSingle();
            Container.BindInstance(Projectile.IceMove).AsSingle();

            InstallGameplayParams();

            InstallAsteroidB();
            InstallAsteroidM();
            InstallAsteroidS();

            InstallIceM();
        }

        private void InstallGameplayParams()
        {
            Container.BindInstance(GameplayParams.Difficult).AsSingle();
        }

        private void InstallPlayer()
        {
            Container.BindInstance(Player.MainSettings).AsSingle();
            Container.BindInstance(Player.Weapon).AsSingle();
            Container.BindInstance(Player.Move).AsSingle();
            Container.BindInstance(Player.Hits).AsSingle();
            Container.BindInstance(Player.Haste).AsSingle();
            Container.BindInstance(Player.Damage).AsSingle();
            Container.BindInstance(Player.TakeDamageAnimation).AsSingle();
            Container.BindInstance(Player.Invincibility).AsSingle();
        }

        private void InstallAsteroidB()
        {
            Container
                .BindInstance(SpawnerSettings.AsteroidBSpawner)
                .WithId("AsteroidB");

            Container
                .BindInstance(Enemies.AsteroidBig.MainSettings)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidBig.Move)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidBig.Hits)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidBig.Damage)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidBig.Rotate)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidBig.View)
                .AsSingle();
        }

        private void InstallAsteroidM()
        {
            Container
                .BindInstance(SpawnerSettings.AsteroidMSpawner)
                .WithId("AsteroidM");

            Container
                .BindInstance(Enemies.AsteroidMedium.MainSettings)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidMedium.Move)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidMedium.Hits)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidMedium.Damage)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidMedium.Rotate)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidMedium.View)
                .AsSingle();
        }

        private void InstallAsteroidS()
        {

            Container
                .BindInstance(SpawnerSettings.AsteroidSSpawner)
                .WithId("AsteroidS");

            Container
                .BindInstance(Enemies.AsteroidSmall.MainSettings)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidSmall.Move)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidSmall.Hits)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidSmall.Damage)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidSmall.Rotate)
                .AsSingle();
            Container
                .BindInstance(Enemies.AsteroidSmall.View)
                .AsSingle();
        }

        private void InstallIceM()
        {
            Container
                .BindInstance(SpawnerSettings.IceMSpawner)
                .WithId("IceM");

            Container
                .BindInstance(Enemies.IceMedium.MainSettings)
                .AsSingle();
            Container
                .BindInstance(Enemies.IceMedium.Move)
                .AsSingle();
            Container
                .BindInstance(Enemies.IceMedium.Hits)
                .AsSingle();
            Container
                .BindInstance(Enemies.IceMedium.Damage)
                .AsSingle();
            Container
                .BindInstance(Enemies.IceMedium.Shooter)
                .AsSingle();
        }
    }
}