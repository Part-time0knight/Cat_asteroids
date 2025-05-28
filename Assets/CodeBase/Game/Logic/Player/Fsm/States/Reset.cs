using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;

namespace Game.Logic.Player.Fsm.States
{
    public class Reset : IState
    {
        private readonly IHandlerGetter _handlerGetter;
        private readonly IGameStateMachine _stateMachine;

        public Reset(IGameStateMachine stateMachine, 
            IHandlerGetter handlerGetter)
        { 
            _handlerGetter = handlerGetter;
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            _handlerGetter.Get<IPlayerDamageHandler>().Reset();
            _stateMachine.Enter<Idle>();
        }

        public void OnExit()
        {
        }
    }
}