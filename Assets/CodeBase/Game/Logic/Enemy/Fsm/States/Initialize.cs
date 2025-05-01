using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly EnemyDamageHandler _damageHandler;
        private readonly AsteroidViewHandler _viewHandler;


        public Initialize(IGameStateMachine stateMachine,
            EnemyDamageHandler enemyDamageHandler,
            AsteroidViewHandler viewHandler)
        {
            _stateMachine = stateMachine;
            _damageHandler = enemyDamageHandler;
            _viewHandler = viewHandler;
        }

        public void OnEnter()
        {
            _damageHandler.Reset();

            _stateMachine.Enter<Run>();
            _viewHandler.Initialize();
        }

        public void OnExit()
        {

        }
    }
}