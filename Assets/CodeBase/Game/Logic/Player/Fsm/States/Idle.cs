using Core.Infrastructure.GameFsm;

namespace Game.Logic.Player.Fsm.States
{
    public class Idle : Hitable
    {
        private readonly PlayerInput _playerInput;
        private readonly PlayerShootHandler _playerShoot;


        public Idle(IGameStateMachine stateMachine,
            PlayerInput playerInput, 
            PlayerDamageHandler damageHandler,
            PlayerShootHandler playerShoot,
            PlayerMoveHandler moveHandler,
            PlayerWeaponHandler weaponHandler) 
            : base(stateMachine,
                  damageHandler,
                  moveHandler,
                  weaponHandler)
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