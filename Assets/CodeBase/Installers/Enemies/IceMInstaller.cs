using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy;
using Game.Logic.Enemy.Fsm;
using Game.Logic.Enemy.Ice;
using Game.Logic.Enemy.Ice.Fsm;
using Game.Logic.Enemy.Ice.IceM;
using Zenject;

namespace Installers.Enemies
{
    public class IceMInstaller : EnemyInstaller
    {
        protected override void InstallFsm()
        {
            Container
                .Bind<EnemyFsm>()
                .To<IceFsm>()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<IInitializable>()
                .FromResolveGetter<EnemyFsm>(fsm => fsm);
            Container
                .Bind<IGameStateMachine>()
                .FromResolveGetter<EnemyFsm>(fsm => fsm);
        }

        protected override void InstallEnemyComponents()
        {
            Container
                .BindInstance(_settings.Body)
                .AsSingle();
            Container
                .BindInstance(_settings.Body.transform)
                .AsSingle();

            Container
                .Bind<EnemyWeaponHandler>()
                .To<IceMWeaponHandler>()
                .AsSingle();
            Container
                .Bind<IceMoveHandler>()
                .To<IceMMoveHandler>()
                .AsSingle();
            Container
                .Bind<EnemyDamageHandler>()
                .To<IceMDamageHandler>()
                .AsSingle();

            Container
                .Bind<IceShootHandler>()
                .To<IceMShootHandler>()
                .AsSingle();

            Container
                .Bind<EnemyMoveHandler>()
                .FromResolveGetter<IceMoveHandler>(fsm => fsm);
            Container
                .Bind<IFixedTickable>()
                .FromResolveGetter<IceMoveHandler>(fsm => fsm);

        }

    }
}