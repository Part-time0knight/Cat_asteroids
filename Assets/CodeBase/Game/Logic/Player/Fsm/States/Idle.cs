using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;

namespace Game.Logic.Player.Fsm.States
{
    public class Idle : Hitable
    {
        private readonly BundleInput _bundleInput;

        public Idle(IGameStateMachine stateMachine,
            PlayerFacade playerHandler,
            IHandlerGetter handlerGetter,
            BundleInput bundleInput) 
            : base(stateMachine,
                  playerHandler,
                  handlerGetter)
        {
            _bundleInput = bundleInput;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _handlerGetter
                .Get<IInputHandler>().InvokeMoveButtonsDown += OnMoveBegin;
            _playerFacade.OnActiveShootChange += InvokeShooting;
            _playerFacade.OnPause += InvokePause;
            InvokeShooting(_playerFacade.ActiveShooting);

            _bundleInput.Active = true;
        }

        public override void OnExit()
        {
            base.OnExit();

            _bundleInput.Active = false;

            _handlerGetter
                .Get<IInputHandler>().InvokeMoveButtonsDown -= OnMoveBegin;
            _playerFacade.OnActiveShootChange -= InvokeShooting;
            _playerFacade.OnPause -= InvokePause;
            InvokeShooting(false);
        }

        private void OnMoveBegin()
        {
            _stateMachine.Enter<Run>();
        }

        private void InvokeShooting(bool active)
        {
            if (active)
                _handlerGetter
                    .Get<IPlayerShootHandler>()
                    .SetTarget(_handlerGetter.Get<IPlayerTargetHandler>().GetTarget);

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