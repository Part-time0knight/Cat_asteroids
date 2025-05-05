using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;
using UnityEngine.InputSystem;

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
            _playerHandler.OnActiveShootChange += InvokeShooting;
            InvokeShooting(_playerHandler.ActiveShooting);
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerShoot.StopAutomatic();
            _playerInput.InvokeMoveButtonsDown -= OnMoveBegin;
            _playerHandler.OnActiveShootChange -= InvokeShooting;
        }

        private void OnMoveBegin()
        {
            _stateMachine.Enter<Run>();
        }

        private void InvokeShooting(bool active)
        {
            if (active)
                _playerShoot.StartAutomatic();
            else
                _playerShoot.StopAutomatic();
        }
    }
}