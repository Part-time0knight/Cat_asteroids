using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Player.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly PlayerDamageHandler _playerDamageHandler;

        public Initialize(IGameStateMachine stateMachine,
            PlayerDamageHandler playerDamageHandler)
        {
            _stateMachine = stateMachine;
            _playerDamageHandler = playerDamageHandler;
        }

        public void OnEnter()
        {
            _playerDamageHandler.Reset();
            _stateMachine.Enter<Idle>();
        }

        public void OnExit()
        {
            
        }
    }
}