using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;

namespace Game.Logic.Player.Fsm.States
{
    public class Idle : Hitable
    {
        private readonly PlayerInput _playerInput;
        private readonly PlayerShootHandler _playerShoot;


        public Idle(IGameStateMachine stateMachine,
            PlayerHandler playerHandler,
            PlayerInput playerInput, 
            PlayerDamageHandler damageHandler,
            PlayerShootHandler playerShoot,
            PlayerMoveHandler moveHandler,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage takeDamageAnimation,
            PlayerInvincibilityHandler playerInvincibility) 
            : base(stateMachine,
                  damageHandler,
                  moveHandler,
                  weaponHandler,
                  takeDamageAnimation,
                  playerHandler,
                  playerInvincibility)
        {
            _playerInput = playerInput;
            _playerShoot = playerShoot;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveButtonsDown += OnMoveBegin;
            _playerShoot.StartAutomatic();
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerShoot.StopAutomatic();
            _playerInput.InvokeMoveButtonsDown -= OnMoveBegin;
        }

        private void OnMoveBegin()
        {
            _stateMachine.Enter<Run>();
        }
    }
}