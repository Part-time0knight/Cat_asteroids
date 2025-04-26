using Game.Logic.Handlers;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerHandler : UnitHandler
    {
        private PlayerDamageHandler _damageHandler;

        public override void MakeCollizion(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(PlayerDamageHandler damageHandler)
        {
            _damageHandler = damageHandler;
        }
    }
}