using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player
{
    public class PlayerHandler : UnitHandler
    {
        public event Action<int> OnTakeDamage;

        public override void MakeCollizion(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            OnTakeDamage?.Invoke(damage);
        }
    }
}