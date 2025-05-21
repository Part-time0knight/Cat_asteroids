using Game.Logic.Enemy.Ice;
using Game.Logic.Misc;
using System;
using UnityEngine;

namespace Installers.Enemies
{
    public class IceProjectileInstaller : ProjectileInstaller
    {
        [SerializeField] private IceSettings _iceSettings;

        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.BindInstance(_iceSettings.Particles).AsSingle();
        }

        protected override void InstallMoveHandler()
        {
            Container
                .BindInterfacesAndSelfTo<IceBulletMoveHandler>()
                .AsSingle();

            Container.Bind<BulletMoveHandler>()
                .FromResolveGetter<IceBulletMoveHandler>(fsm => fsm);
        }

        [Serializable]
        public class IceSettings
        {
            [field: SerializeField] public ParticleSystem Particles;
        }
    }
}