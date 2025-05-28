using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Animation;

namespace Game.Logic.Player.Fsm.States
{
    public class Pause : IState
    {
        private readonly PlayerHandler _playerHandler;
        private readonly IGameStateMachine _playerFsm;
        private readonly IHandlerGetter _handlerGetter;
        private readonly PlayerTakeDamage _playerTakeDamage;
        private readonly PlayerInvincibilityHandler _playerInvincibilityHandler;

        public Pause(IGameStateMachine playerFsm,
            PlayerHandler playerHandler,
            PlayerTakeDamage playerTakeDamage,
            PlayerInvincibilityHandler playerInvincibilityHandler,
            IHandlerGetter handlerGetter)
        {
            _playerHandler = playerHandler;
            _playerInvincibilityHandler = playerInvincibilityHandler;
            _playerTakeDamage = playerTakeDamage;
            _playerFsm = playerFsm;
            _handlerGetter = handlerGetter;
        }

        public void OnEnter()
        {
            _handlerGetter.Get<IPlayerMoveHandler>().Pause();
            _playerTakeDamage.Pause();
            _handlerGetter.Get<IPlayerShootHandler>().IsPause = true;
            _playerInvincibilityHandler.Pause();
            _playerHandler.OnPause += InvokePause;
        }

        public void OnExit()
        {
            _handlerGetter.Get<IPlayerMoveHandler>().Continue();
            _handlerGetter.Get<IPlayerShootHandler>().IsPause = false;
            _playerInvincibilityHandler.Continue();
            _playerTakeDamage.Continue();
            _playerHandler.OnPause -= InvokePause;
        }

        private void InvokePause(bool active)
        {
            if (active) return;
            _playerFsm.Enter<Idle>();
        }
    }
}