using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy.Fsm.States;
using Game.Logic.Enemy.Ice.IceM;

namespace Game.Logic.Enemy.Ice.Fsm.States
{
    public class IceInitialize : Initialize
    {
        private readonly IceMoveHandler _moveHandler;
        private readonly EnemyHandler _enemyHandler;

        public IceInitialize(IGameStateMachine stateMachine,
            EnemyDamageHandler enemyDamageHandler,
            IceMoveHandler enemyMoveHandler,
            EnemyHandler enemyHandler) : 
            base(stateMachine,
                enemyDamageHandler, 
                enemyMoveHandler)
        {
            _moveHandler = enemyMoveHandler;
            _enemyHandler = enemyHandler;
        }

        public override void OnEnter()
        {
            _moveHandler.SetInitialVelocity(_enemyHandler.Direction);
            base.OnEnter();
        }
    }
}