using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy.Fsm.States;
using Game.Logic.Handlers;

namespace Game.Logic.Enemy.Ice.Fsm.States
{
    public class IceInitialize : Initialize
    {
        private readonly IceMoveHandler _moveHandler;
        private readonly EnemyHandler _enemyHandler;
        private readonly ProjectileManager _projectileManager;
        private readonly IceBullet.IcePool _icePool;

        public IceInitialize(IGameStateMachine stateMachine,
            EnemyDamageHandler enemyDamageHandler,
            IceMoveHandler enemyMoveHandler,
            EnemyHandler enemyHandler,
            ProjectileManager projectileManager,
            IceBullet.IcePool icePool) : 
            base(stateMachine,
                enemyDamageHandler, 
                enemyMoveHandler)
        {
            _moveHandler = enemyMoveHandler;
            _enemyHandler = enemyHandler;
            _projectileManager = projectileManager;
            _icePool = icePool;
        }

        public override void OnEnter()
        {
            _moveHandler.SetInitialVelocity(_enemyHandler.Direction);
            _projectileManager.Pool = _icePool;
            base.OnEnter();
        }
    }
}