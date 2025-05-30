using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy.Fsm.States;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.Fsm.States
{
    public class AsteroidRun : Run
    {

        private readonly AsteroidRotate _rotate;

        public AsteroidRun(IGameStateMachine stateMachine, 
            EnemyFacade enemyHandler, 
            EnemyMoveHandler moveHandler,
            EnemyWeaponHandler weapon,
            EnemyDamageHandler damageHandler,
            AsteroidRotate rotate) 
            : base(stateMachine, 
                  enemyHandler,
                  moveHandler,
                  weapon,
                  damageHandler)
        {
            _rotate = rotate;
        }
        public override void OnEnter()
        {
            _rotate.Play();
            base.OnEnter();
        }

        public override void OnExit()
        {
            _rotate.Stop();
            base.OnExit();
        }
    }
}