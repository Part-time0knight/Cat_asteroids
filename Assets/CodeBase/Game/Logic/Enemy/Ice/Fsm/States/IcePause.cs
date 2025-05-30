using Core.Infrastructure.GameFsm;
using Game.Logic.Enemy.Fsm.States;

namespace Game.Logic.Enemy.Ice.Fsm.States
{
    public class IcePause : Pause
    {
        private readonly IceShootHandler _iceShootHandler;

        public IcePause(IGameStateMachine stateMachine,
            EnemyFacade enemy, IceShootHandler iceShootHandler)
            : base(stateMachine,
                  enemy)
        {
            _iceShootHandler = iceShootHandler;
        }

        public override void OnEnter()
        {
            _iceShootHandler.Pause();
            base.OnEnter();
        }

        public override void OnExit()
        {
            _iceShootHandler.Continue();
            base.OnExit();
        }
    }
}