using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy.Fsm.States;
using UnityEngine;

namespace Game.Logic.Enemy.Ice.Fsm.States
{
    public class IceRun : Run
    {
        private readonly IceShootHandler _iceShootHandler;

        public IceRun(IGameStateMachine stateMachine,
            EnemyHandler enemyHandler, 
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
            base.OnEnter();
        }

        public override void OnExit()
        {
            _iceShootHandler.StopAutomatic();
            base.OnExit();
        }

    }
}