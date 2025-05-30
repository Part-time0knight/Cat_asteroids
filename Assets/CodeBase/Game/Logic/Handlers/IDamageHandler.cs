using System;

namespace Game.Logic.Handlers
{
    public interface IDamageHandler : IHandler
    {
        event Action<int> OnTakeDamage;
        event Action OnDeath;

        void TakeDamage(int damage);

        void Reset();
    }
}