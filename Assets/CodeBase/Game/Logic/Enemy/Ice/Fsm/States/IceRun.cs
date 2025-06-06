using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy.Fsm.States;
using Game.Logic.Handlers;
using UnityEngine;

namespace Game.Logic.Enemy.Ice.Fsm.States
{
    public class IceRun : Run
    {
        private readonly IceShootHandler _iceShootHandler;

        public IceRun(IGameStateMachine stateMachine,
            EnemyFacade enemyHandler, 
            EnemyMoveHandler moveHandler, 
            EnemyWeaponHandler weapon,
            EnemyDamageHandler damageHandler,
            IceShootHandler iceShootHandler) 
            : base(stateMachine,
                  enemyHandler,
                  moveHandler, 
                  weapon,
                  damageHandler)
        {
            _iceShootHandler = iceShootHandler;
        }

        public override void OnEnter()
        {
            _iceShootHandler.StartAutomatic();
            _moveHandler.OnCollision += Hit;
            _damageHandler.OnDeath += InvokeDeath;
            _moveHandler.OnTrigger += InvokeTrigger;
            _enemyHandler.OnDamaged += InvokeDamaged;
            _enemyHandler.OnPause += InvokePause;
            _moveHandler.Move(_enemyHandler.Direction);
            InvokePause(_enemyHandler.Pause);
        }

        public override void OnExit()
        {
            _iceShootHandler.StopAutomatic();
            base.OnExit();
        }

    }
}