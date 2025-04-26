using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Player.Fsm.States
{
    public abstract class Hitable : IState
    {
        protected readonly IGameStateMachine _stateMachine;
        protected readonly PlayerDamageHandler.PlayerSettings _damageSettings;
        public Hitable(IGameStateMachine stateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings)
        {
            _stateMachine = stateMachine;
            _damageSettings = damageSettings;
        }

        public virtual void OnEnter()
        {
            _damageSettings.InvokeHitPointsChange += OnHit;
        }

        public virtual void OnExit()
        {
            _damageSettings.InvokeHitPointsChange -= OnHit;
        }

        protected virtual void OnHit()
        {
            if (_damageSettings.CurrentHits > 0)
                return;
            _stateMachine.Enter<Dead>();
        }
    }
}