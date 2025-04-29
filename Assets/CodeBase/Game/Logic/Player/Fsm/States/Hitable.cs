using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Player.Fsm.States
{
    public abstract class Hitable : IState
    {
        protected readonly IGameStateMachine _stateMachine;
        protected readonly PlayerDamageHandler _damageHandler;
        public Hitable(IGameStateMachine stateMachine,
            PlayerDamageHandler damageHandler)
        {
            _stateMachine = stateMachine;
            _damageHandler = damageHandler;
        }

        public virtual void OnEnter()
        {
            _damageHandler.OnDeath += OnHit;
        }

        public virtual void OnExit()
        {
            _damageHandler.OnDeath -= OnHit;
        }

        protected virtual void OnHit()
        {
            _stateMachine.Enter<Dead>();
        }
    }
}