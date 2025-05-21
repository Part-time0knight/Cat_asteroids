using Game.Logic.Enemy.Ice;
using Game.Logic.Misc;

namespace Installers.Enemies
{
    public class IceProjectileInstaller : ProjectileInstaller
    {
        protected override void InstallMoveHandler()
        {
            Container
                .BindInterfacesAndSelfTo<IceBulletMoveHandler>()
                .AsSingle();

            Container.Bind<BulletMoveHandler>()
                .FromResolveGetter<IceBulletMoveHandler>(fsm => fsm);
        }
    }
}