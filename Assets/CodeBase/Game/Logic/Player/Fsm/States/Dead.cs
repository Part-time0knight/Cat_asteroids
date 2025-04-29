using Core.Infrastructure.GameFsm;

namespace Game.Logic.Player.Fsm.States
{
    public class Dead : Hitable
    {

        public Dead(IGameStateMachine stateMachine,
            PlayerDamageHandler damageHandler) : base(stateMachine, damageHandler)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnHit()
        {
            //_stateMachine.Enter<Idle>();
        }
    }
}
