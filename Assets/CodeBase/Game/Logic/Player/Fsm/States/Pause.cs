using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;

namespace Game.Logic.Player.Fsm.States
{
    public class Pause : IState
    {
        private readonly PlayerFacade _playerHandler;
        private readonly IGameStateMachine _playerFsm;
        private readonly IHandlerGetter _handlerGetter;

        public Pause(IGameStateMachine playerFsm,
            PlayerFacade playerHandler,
            IHandlerGetter handlerGetter)
        {
            _playerHandler = playerHandler;
            _playerFsm = playerFsm;
            _handlerGetter = handlerGetter;
        }

        public void OnEnter()
        {
            _handlerGetter.Get<IPlayerMoveHandler>().Pause();
            _handlerGetter.Get<IPlayerDamageHandler>().Pause();
            _handlerGetter.Get<IPlayerShootHandler>().IsPause = true;
            _handlerGetter.Get<IInvincibilityHandler>().Pause();
            _playerHandler.OnPause += InvokePause;
        }

        public void OnExit()
        {
            _handlerGetter.Get<IPlayerMoveHandler>().Continue();
            _handlerGetter.Get<IPlayerShootHandler>().IsPause = false;
            _handlerGetter.Get<IInvincibilityHandler>().Continue();
            _handlerGetter.Get<IPlayerDamageHandler>().Continue();
            _playerHandler.OnPause -= InvokePause;
        }

        private void InvokePause(bool active)
        {
            if (active) return;
            _playerFsm.Enter<Idle>();
        }
    }
}