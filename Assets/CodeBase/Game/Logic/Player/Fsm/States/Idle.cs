using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;

namespace Game.Logic.Player.Fsm.States
{
    public class Idle : Hitable
    {
        public Idle(IGameStateMachine stateMachine,
            PlayerFacade playerHandler,
            IHandlerGetter handlerGetter) 
            : base(stateMachine,
                  playerHandler,
                  handlerGetter)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _handlerGetter
                .Get<IInputHandler>().InvokeMoveButtonsDown += OnMoveBegin;
            _playerFacade.OnActiveShootChange += InvokeShooting;
            _playerFacade.OnPause += InvokePause;
            InvokeShooting(_playerFacade.ActiveShooting);
        }

        public override void OnExit()
        {
            base.OnExit();

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
            {
                _handlerGetter
                    .Get<IPlayerShootHandler>()
                    .SetTarget(_handlerGetter.Get<IPlayerTargetHandler>().GetTarget);

                _handlerGetter
                    .Get<IPlayerShootHandler>()
                    .SetPosition(_handlerGetter.Get<IPlayerTargetHandler>().GetPosition);
            }
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