using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;

namespace Game.Logic.Player.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IHandlerSetter _handlerSetter;

        public Initialize(IGameStateMachine stateMachine,
            IHandlerSetter handlerSetter)
        {
            _stateMachine = stateMachine;
            _handlerSetter = handlerSetter;
        }

        public void OnEnter()
        {
            HandlersResolve();
            _stateMachine.Enter<Idle>();
        }

        public void OnExit()
        {
            
        }

        private void HandlersResolve()
        {
            _handlerSetter.Set<PlayerInvincibilityHandler, IInvincibilityHandler>();
            _handlerSetter.Set<PlayerBaseShootHandler, IPlayerShootHandler>();
            _handlerSetter.Set<PlayerBaseMoveHandler, IPlayerMoveHandler>();
            _handlerSetter.Set<PlayerDamageHandler, IPlayerDamageHandler>();
            _handlerSetter.Set<PlayerWeaponHandler, IWeaponHandler>();
            _handlerSetter.Set<PlayerInputHandler, IInputHandler>();

        }
    }
}