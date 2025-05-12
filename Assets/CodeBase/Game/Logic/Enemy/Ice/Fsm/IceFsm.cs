using Core.Domain.Factories;
using Game.Logic.Enemy.Fsm;
using Game.Logic.Enemy.Fsm.States;
using Game.Logic.Enemy.Ice.Fsm.States;

namespace Game.Logic.Enemy.Ice.Fsm
{
    public class IceFsm : EnemyFsm
    {
        public IceFsm(IStatesFactory factory) : base(factory)
        {
        }

        protected override void StateResolve()
        {
            _states.Add(typeof(Initialize), _factory.Create<Initialize>());
            _states.Add(typeof(Run), _factory.Create<IceRun>());
            _states.Add(typeof(Pause), _factory.Create<IcePause>());
            _states.Add(typeof(Dead), _factory.Create<Dead>());
        }
    }
}