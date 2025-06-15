
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;

namespace Game.Logic.Player.Mutators.TargetMutators
{
    public class BaseTarget
    {
        private readonly IHandlerSetter _handlerSetter;

        public BaseTarget(IHandlerSetter handlerSetter)
        {
            _handlerSetter = handlerSetter;
        }

        public void Set()
        {
            _handlerSetter
                .Set<PlayerTargetHandler, IPlayerTargetHandler>();
        }


    }
}