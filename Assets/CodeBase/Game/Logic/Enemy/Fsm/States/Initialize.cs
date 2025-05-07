using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy.Asteroid;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly EnemyDamageHandler _damageHandler;
        private readonly AsteroidViewHandler _viewHandler;
        private readonly EnemyMoveHandler _enemyMoveHandler;
        private readonly AsteroidRotate _asteroidRotate;

        public Initialize(IGameStateMachine stateMachine,
            EnemyDamageHandler enemyDamageHandler,
            AsteroidViewHandler viewHandler,
            EnemyMoveHandler enemyMoveHandler,
            AsteroidRotate asteroidRotate)
        {
            _stateMachine = stateMachine;
            _enemyMoveHandler = enemyMoveHandler;
            _damageHandler = enemyDamageHandler;
            _viewHandler = viewHandler;
            _asteroidRotate = asteroidRotate;
        }

        public void OnEnter()
        {
            _damageHandler.Reset();
            _enemyMoveHandler.Initialize();
            _stateMachine.Enter<Run>();
            _viewHandler.Initialize();
            _asteroidRotate.Initialize();
        }

        public void OnExit()
        {

        }
    }
}