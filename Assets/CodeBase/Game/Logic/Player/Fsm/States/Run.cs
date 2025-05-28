using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;


namespace Game.Logic.Player.Fsm.States
{
    public class Run : Hitable
    {
        private readonly IPlayerShootHandler _playerShoot;
        private readonly PlayerInput _playerInput;
        private readonly PlayerMoveHandler _playerMove;

        public Run(IGameStateMachine stateMachine,
            PlayerInput playerInput,
            PlayerHandler playerHandler,
            PlayerMoveHandler playerMove, 
            PlayerDamageHandler damageHandler,
            IPlayerShootHandler playerShoot,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage takeDamageAnimation,
            PlayerInvincibilityHandler playerInvincibility) 
            : base(stateMachine,
                damageHandler,
                playerMove,
                weaponHandler,
                takeDamageAnimation,
                playerHandler,
                playerInvincibility)
        {
            _playerInput = playerInput;
            _playerMove = playerMove;
            _playerShoot = playerShoot;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveVertical += Move;
            _playerInput.InvokeMoveHorizontal += Rotate;
            
            InvokeShooting(_playerHandler.ActiveShooting);
            _playerHandler.OnActiveShootChange += InvokeShooting;
            _playerHandler.OnPause += InvokePause;
        }

        private void Move(float speed)
        {
            if (speed > 0)
                _playerMove.Move();
            else
                _playerMove.ReverseMove();
        }

        private void Rotate(float speed)
        {
            _playerMove.Rotate(speed);
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeMoveVertical -= Move;
            _playerInput.InvokeMoveHorizontal -= Rotate;
            _playerHandler.OnActiveShootChange -= InvokeShooting;
            _playerHandler.OnPause -= InvokePause;
            InvokeShooting(false);
        }
        private void InvokeShooting(bool active)
        {
            _playerShoot.Active = active;
        }

        protected virtual void InvokePause(bool isPause)
        {
            if (!isPause)
                return;

            _stateMachine.Enter<Pause>();
        }
    }
}