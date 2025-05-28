using Core.Infrastructure.GameFsm;
using Game.Logic.Effects.Particles;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Animation;


namespace Game.Logic.Player.Fsm.States
{
    public class Run : Hitable
    {
        private readonly PlayerInput _playerInput;
        private readonly PlayerHasteEffectHandler _hasteEffectHandler;

        public Run(IGameStateMachine stateMachine,
            PlayerInput playerInput,
            PlayerHandler playerHandler,
            PlayerDamageHandler damageHandler,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage takeDamageAnimation,
            PlayerInvincibilityHandler playerInvincibility,
            PlayerHasteEffectHandler hasteEffectHandler,
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
            _hasteEffectHandler = hasteEffectHandler;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveVertical += Move;
            _playerInput.InvokeMoveHorizontal += Rotate;
            
            InvokeShooting(_playerHandler.ActiveShooting);
            _playerHandler.OnActiveShootChange += InvokeShooting;
            _playerHandler.OnPause += InvokePause;

            _handlerGetter.Get<IPlayerMoveHandler>().OnHaste += _hasteEffectHandler.InvokeHaste;
        }

        private void Move(float speed)
        {
            if (speed > 0)
                _handlerGetter.Get<IPlayerMoveHandler>().Move();
            else
                _handlerGetter.Get<IPlayerMoveHandler>().ReverseMove();
        }

        private void Rotate(float speed)
        {
            _handlerGetter.Get<IPlayerMoveHandler>().Rotate(speed);
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeMoveVertical -= Move;
            _playerInput.InvokeMoveHorizontal -= Rotate;
            _playerHandler.OnActiveShootChange -= InvokeShooting;
            _playerHandler.OnPause -= InvokePause;

            _handlerGetter.Get<IPlayerMoveHandler>().OnHaste -= _hasteEffectHandler.InvokeHaste;
            InvokeShooting(false);
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