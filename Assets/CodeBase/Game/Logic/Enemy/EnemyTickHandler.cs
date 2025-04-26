using System;
using Zenject;

namespace Game.Logic.Enemy
{
    public class EnemyTickHandler : IFixedTickable, ITickable
    {
        public event Action OnTick;
        public event Action OnFixedTick;

        public void Tick()
            => OnTick?.Invoke();

        public void FixedTick()
            => OnFixedTick?.Invoke();

    }
}