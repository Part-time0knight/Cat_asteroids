using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;


namespace Game.Logic.Player.Fsm.States
{
    public class Run : Hitable
    {
        private readonly PlayerShootHandler _playerShoot;
        private readonly PlayerInput _playerInput;
        private readonly PlayerMoveHandler _playerMove;

        public Run(IGameStateMachine stateMachine,
            PlayerInput playerInput,
            PlayerMoveHandler playerMove, 
            PlayerDamageHandler damageHandler,
            PlayerShootHandler playerShoot,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage takeDamageAnimation) 
            : base(stateMachine,
                damageHandler,
                playerMove,
                weaponHandler,
                takeDamageAnimation)
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
            _playerShoot.StartAutomatic();
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
            _playerShoot.StopAutomatic();
        }


    }
}