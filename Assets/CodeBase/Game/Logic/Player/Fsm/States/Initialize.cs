using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers.Strategy;

namespace Game.Logic.Player.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IHandlerSetter _handlerSetter;
        private readonly PlayerDamageHandler _playerDamageHandler;

        public Initialize(IGameStateMachine stateMachine,
            PlayerDamageHandler playerDamageHandler,
            IHandlerSetter handlerSetter)
        {
            _stateMachine = stateMachine;
            _playerDamageHandler = playerDamageHandler;
            _handlerSetter = handlerSetter;
        }

        public void OnEnter()
        {
            _playerDamageHandler.Reset();
            HandlersResolve();
            _stateMachine.Enter<Idle>();
        }

        public void OnExit()
        {
            
        }

        private void HandlersResolve()
        {
            _handlerSetter.Set<PlayerBaseShootHandler, IPlayerShootHandler>();
            _handlerSetter.Set<PlayerBaseMoveHandler, IPlayerMoveHandler>();

        }
    }
}