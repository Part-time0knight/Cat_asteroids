using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly EnemyDamageHandler _damageHandler;
        private readonly EnemyMoveHandler _enemyMoveHandler;

        public Initialize(IGameStateMachine stateMachine,
            EnemyDamageHandler enemyDamageHandler,
            EnemyMoveHandler enemyMoveHandler)
        {
            _stateMachine = stateMachine;
            _enemyMoveHandler = enemyMoveHandler;
            _damageHandler = enemyDamageHandler;
        }

        public virtual void OnEnter()
        {
            _damageHandler.Reset();
            _enemyMoveHandler.Initialize();
            _stateMachine.Enter<Run>();
        }

        public virtual void OnExit()
        {

        }
    }
}