using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy.Fsm.States;

namespace Game.Logic.Enemy.Asteroid.Fsm.States
{
    public class AsteroidInitialize : Initialize
    {
        private readonly AsteroidViewHandler _viewHandler;
        private readonly AsteroidRotate _asteroidRotate;

        public AsteroidInitialize(IGameStateMachine stateMachine,
            EnemyDamageHandler enemyDamageHandler,
            AsteroidViewHandler viewHandler,
            EnemyMoveHandler enemyMoveHandler,
            AsteroidRotate asteroidRotate)
            : base(stateMachine, 
                  enemyDamageHandler,
                  enemyMoveHandler)
        {
            _viewHandler = viewHandler;
            _asteroidRotate = asteroidRotate;
        }

        public override void OnEnter()
        {
            _viewHandler.Initialize();
            _asteroidRotate.Initialize();
            base.OnEnter();
        }
    }
}