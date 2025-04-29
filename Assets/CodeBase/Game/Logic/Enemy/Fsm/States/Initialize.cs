using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Handlers;
using Game.Presentation.View;
using UnityEngine.EventSystems;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowResolve _windowResolve;
        private readonly EnemyDamageHandler _damageHandler;


        public Initialize(IGameStateMachine stateMachine,
            IWindowResolve windowResolve,
            EnemyDamageHandler enemyDamageHandler)
        {
            _stateMachine = stateMachine;
            _windowResolve = windowResolve;
            _damageHandler = enemyDamageHandler;
        }

        public void OnEnter()
        {
            WindowResolve();
            _damageHandler.Reset();

            _stateMachine.Enter<Run>();
        }

        public void OnExit()
        {

        }

        private void WindowResolve()
        {
            //_windowResolve.Set<EnemyView>();
        }
    }
}