using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Player.Fsm.States
{
    public class Pause : IState
    {
        private readonly PlayerMoveHandler _playerMove;
        private readonly PlayerHandler _playerHandler;
        private readonly IGameStateMachine _playerFsm;
        private readonly PlayerShootHandler _playerShootHandler;

        public Pause(IGameStateMachine playerFsm,
            PlayerMoveHandler playerMove,
            PlayerHandler playerHandler,
            PlayerShootHandler playerShootHandler)
        {
            _playerMove = playerMove;
            _playerHandler = playerHandler;
            _playerShootHandler = playerShootHandler;
            _playerFsm = playerFsm;
        }

        public void OnEnter()
        {
            _playerMove.Pause();
            _playerShootHandler.Pause();
            _playerHandler.OnPause += InvokePause;
        }

        public void OnExit()
        {
            _playerMove.Continue();
            _playerShootHandler.Continue();
            _playerHandler.OnPause -= InvokePause;
        }

        private void InvokePause(bool active)
        {
            if (active) return;
            _playerFsm.Enter<Idle>();
        }
    }
}