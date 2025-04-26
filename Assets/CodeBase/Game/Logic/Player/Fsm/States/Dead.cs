using Core.Infrastructure.GameFsm;
using Game.Logic.StaticData;

namespace Game.Logic.Player.Fsm.States
{
    public class Dead : Hitable
    {

        public Dead(IGameStateMachine stateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings) : base(stateMachine, damageSettings)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnHit()
        {
            if (_damageSettings.CurrentHits <= 0)
                return;
            _stateMachine.Enter<Idle>();
        }
    }
}
