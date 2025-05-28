using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;


namespace Game.Logic.Player.Fsm.States
{
    public class Run : Hitable
    {
        public Run(IGameStateMachine stateMachine,
            PlayerFacade playerFacade,
            IHandlerGetter handlerGetter) 
            : base(stateMachine,
                playerFacade,
                handlerGetter)
        {
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
            _handlerGetter.Get<IPlayerShootHandler>().Active = active;
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