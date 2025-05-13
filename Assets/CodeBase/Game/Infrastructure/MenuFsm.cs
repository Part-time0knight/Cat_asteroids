using Core.Domain.Factories;
using Core.Infrastructure.GameFsm;
using Game.Infrastructure.States.Menu;
using Infrastructure.States.Menu;
using Zenject;

namespace Game.Infrastructure
{
    public class MenuFsm : AbstractGameStateMachine, IInitializable
    {
        public MenuFsm(IStatesFactory factory) : base(factory)
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
            _states.Add(typeof(MainMenu), _factory.Create<MainMenu>());
            _states.Add(typeof(LoadGameplay), _factory.Create<LoadGameplay>());
        }
    }
}