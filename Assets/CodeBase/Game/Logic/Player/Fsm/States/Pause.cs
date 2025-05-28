using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Player.Animation;

namespace Game.Logic.Player.Fsm.States
{
    public class Pause : IState
    {
        private readonly PlayerMoveHandler _playerMove;
        private readonly PlayerHandler _playerHandler;
        private readonly IGameStateMachine _playerFsm;
        private readonly PlayerShootHandler _playerShootHandler;
        private readonly PlayerTakeDamage _playerTakeDamage;
        private readonly PlayerInvincibilityHandler _playerInvincibilityHandler;

        public Pause(IGameStateMachine playerFsm,
            PlayerMoveHandler playerMove,
            PlayerHandler playerHandler,
            PlayerShootHandler playerShootHandler,
            PlayerTakeDamage playerTakeDamage,
            PlayerInvincibilityHandler playerInvincibilityHandler)
        {
            _playerMove = playerMove;
            _playerHandler = playerHandler;
            _playerShootHandler = playerShootHandler;
            _playerInvincibilityHandler = playerInvincibilityHandler;
            _playerTakeDamage = playerTakeDamage;
            _playerFsm = playerFsm;
        }

        public void OnEnter()
        {
            _playerMove.Pause();
            _playerTakeDamage.Pause();
            _playerShootHandler.SetPause();
            _playerInvincibilityHandler.Pause();
            _playerHandler.OnPause += InvokePause;
        }

        public void OnExit()
        {
            _playerMove.Continue();
            _playerShootHandler.Continue();
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