using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;


namespace Game.Logic.Player.Fsm.States
{
    public class Run : Hitable
    {
        private readonly BundleInput _bundleInput;

        public Run(IGameStateMachine stateMachine,
            PlayerFacade playerFacade,
            BundleInput bundleInput,
            IHandlerGetter handlerGetter) 
            : base(stateMachine,
                playerFacade,
                handlerGetter)
        {
            _bundleInput = bundleInput;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _handlerGetter
                .Get<IInputHandler>().InvokeMoveVertical += Move;
            _handlerGetter
                .Get<IInputHandler>().InvokeMoveHorizontal += Rotate;
            _handlerGetter
                .Get<IInputHandler>().InvokeMoveButtonsUp += InvokeEndMove;
            
            InvokeShooting(_playerFacade.ActiveShooting);
            _playerFacade.OnActiveShootChange += InvokeShooting;
            _playerFacade.OnPause += InvokePause;

            _bundleInput.Active = true;
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

            _bundleInput.Active = false;

            _handlerGetter
                .Get<IInputHandler>().InvokeMoveVertical -= Move;
            _handlerGetter
                .Get<IInputHandler>().InvokeMoveHorizontal -= Rotate;
            _handlerGetter
                .Get<IInputHandler>().InvokeMoveButtonsUp -= InvokeEndMove;

            _playerFacade.OnActiveShootChange -= InvokeShooting;

            _playerFacade.OnPause -= InvokePause;

            InvokeShooting(false);
        }
        private void InvokeShooting(bool active)
        {
            if (active)
            {
                _handlerGetter
                    .Get<IPlayerShootHandler>()
                    .SetTarget(_handlerGetter.Get<IPlayerTargetHandler>().GetTarget);

                _handlerGetter
                    .Get<IPlayerShootHandler>()
                    .SetPosition(_handlerGetter.Get<IPlayerTargetHandler>().GetPosition);
            }
            _handlerGetter
                .Get<IPlayerShootHandler>()
                .Active = active;
        }

        protected virtual void InvokePause(bool isPause)
        {
            if (!isPause)
                return;

            _stateMachine.Enter<Pause>();
        }

        private void InvokeEndMove()
        {
            _stateMachine.Enter<Idle>();
        }
    }
}