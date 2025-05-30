using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;

namespace Game.Logic.Player.Fsm.States
{
    public class Pause : Hitable
    {
        private readonly PlayerFacade _playerHandler;
        private readonly IGameStateMachine _playerFsm;

        public Pause(IGameStateMachine playerFsm,
            PlayerFacade playerHandler,
            IHandlerGetter handlerGetter) 
            : base(playerFsm, 
                playerHandler, 
                handlerGetter)
        {
            _playerHandler = playerHandler;
            _playerFsm = playerFsm;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _handlerGetter.Get<IPlayerMoveHandler>().Pause();
            _handlerGetter.Get<IPlayerDamageHandler>().Pause();
            _handlerGetter.Get<IPlayerShootHandler>().IsPause = true;
            _handlerGetter.Get<IInvincibilityHandler>().Pause();
            _playerHandler.OnPause += InvokePause;
        }

        public override void OnExit()
        {
            base.OnExit();
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