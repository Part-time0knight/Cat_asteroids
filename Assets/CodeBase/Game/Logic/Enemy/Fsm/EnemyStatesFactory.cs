using Game.Domain.Factories.GameFsm;
using Zenject;

namespace Game.Logic.Enemy.Fsm
{
    public class EnemyStatesFactory : StatesFactory
    {
        public EnemyStatesFactory(DiContainer container) : base(container)
        {
        }

        
    }
}