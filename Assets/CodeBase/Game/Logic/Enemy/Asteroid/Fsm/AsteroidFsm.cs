using Core.Domain.Factories;
using Game.Logic.Enemy.Asteroid.Fsm.States;
using Game.Logic.Enemy.Fsm;
using Game.Logic.Enemy.Fsm.States;

namespace Game.Logic.Enemy.Asteroid.Fsm
{
    public class AsteroidFsm : EnemyFsm
    {
        public AsteroidFsm(IStatesFactory factory) : base(factory)
        {
        }

        protected override void StateResolve()
        {
            _states.Add(typeof(Initialize), _factory.Create<AsteroidInitialize>());
            _states.Add(typeof(Run), _factory.Create<AsteroidRun>());
            _states.Add(typeof(Pause), _factory.Create<Pause>());
            _states.Add(typeof(Dead), _factory.Create<Dead>());
        }
    }
}