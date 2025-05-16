using Core.Domain.Factories;
using Core.Infrastructure.GameFsm;
using Game.Infrastructure.States.Gameplay;
using Zenject;

namespace Game.Infrastructure
{
    public class GameFsm : AbstractGameStateMachine, IInitializable
    {
        public GameFsm(IStatesFactory factory) : base(factory)
        {
        }

        public void Initialize()
        {
            StateResolve();
            Enter<Initialize>();
        }

        private void StateResolve()
        {
            _states.Add(typeof(Initialize), _factory.Create<Initialize>());
            _states.Add(typeof(Start), _factory.Create<Start>());
            _states.Add(typeof(Pause), _factory.Create<Pause>());
            _states.Add(typeof(GameplayState), _factory.Create<GameplayState>());
            _states.Add(typeof(Defeat), _factory.Create<Defeat>());
            _states.Add(typeof(LoadMenu), _factory.Create<LoadMenu>());
        }
    }
}