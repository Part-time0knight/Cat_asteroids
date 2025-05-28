using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Animation;
using UnityEngine.InputSystem;

namespace Game.Logic.Player.Fsm.States
{
    public class Idle : Hitable
    {
        private readonly PlayerInput _playerInput;

        public Idle(IGameStateMachine stateMachine,
            PlayerHandler playerHandler,
            PlayerInput playerInput, 
            PlayerDamageHandler damageHandler,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage takeDamageAnimation,
            PlayerInvincibilityHandler playerInvincibility,
            IHandlerGetter handlerGetter) 
            : base(stateMachine,
                  damageHandler,
                  weaponHandler,
                  takeDamageAnimation,
                  playerHandler,
                  playerInvincibility,
                  handlerGetter)
        {
            _playerInput = playerInput;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveButtonsDown += OnMoveBegin;
            _playerHandler.OnActiveShootChange += InvokeShooting;
            _playerHandler.OnPause += InvokePause;
            InvokeShooting(_playerHandler.ActiveShooting);
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeMoveButtonsDown -= OnMoveBegin;
            _playerHandler.OnActiveShootChange -= InvokeShooting;
            _playerHandler.OnPause -= InvokePause;
            InvokeShooting(false);
        }

        private void OnMoveBegin()
        {
            _stateMachine.Enter<Run>();
        }

        private void InvokeShooting(bool active)
        {
            _handlerGetter.Get<IPlayerShootHandler>().Active = active;
        }

        protected virtual void InvokePause(bool isPause)
        {
            if (!isPause)
                return;

            _stateMachine.Enter<Pause>();
        }
    }
}