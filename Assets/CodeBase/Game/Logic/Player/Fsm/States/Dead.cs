using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;

namespace Game.Logic.Player.Fsm.States
{
    public class Dead : Hitable
    {

        public Dead(IGameStateMachine stateMachine,
            PlayerDamageHandler damageHandler,
            PlayerMoveHandler moveHandler,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage takeDamageAnimation)
            : base(stateMachine,
                  damageHandler,
                  moveHandler,
                  weaponHandler,
                  takeDamageAnimation)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void InvokeDead()
        {
            //_stateMachine.Enter<Idle>();
        }
    }
}
