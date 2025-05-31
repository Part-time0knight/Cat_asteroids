using Game.Logic.Enemy.Ice;
using Game.Logic.Misc;
using Game.Logic.Projectiles;
using System;
using UnityEngine;

namespace Installers.Projectiles
{
    public class IceProjectileInstaller : ProjectileInstaller
    {
        [SerializeField] private IceSettings _iceSettings;

        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.BindInstance(_iceSettings.Particles).AsSingle();
        }

        protected override void InstallHandlers()
        {
            Container
                .BindInterfacesAndSelfTo<IceBulletMoveHandler>()
                .AsSingle();

            Container.Bind<BulletMoveHandler>()
                .FromResolveGetter<IceBulletMoveHandler>(fsm => fsm);

            Container
                .Bind<ProjectileDamageHandler>()
                .To<IceProjectileDamageHandler>()
                .AsSingle();
        }



        [Serializable]
        public class IceSettings
        {
            [field: SerializeField] public ParticleSystem Particles { get; private set; }
        }
    }
}