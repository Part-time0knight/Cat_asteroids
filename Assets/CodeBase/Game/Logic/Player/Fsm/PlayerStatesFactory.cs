using Game.Domain.Factories.GameFsm;
using Zenject;

namespace Game.Logic.Player.Fsm
{
    public class PlayerStatesFactory : StatesFactory
    {
        public PlayerStatesFactory(DiContainer container) : base(container)
        { }
    }
}