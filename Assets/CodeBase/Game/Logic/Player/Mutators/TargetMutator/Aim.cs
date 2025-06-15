using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Player.Mutators.TargetMutators;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;

namespace Game.Logic.Player.Mutators.TargetMutator
{
    public class Aim : AbstractMutator
    {
        protected override Mutator Id => Mutator.AIM;

        private readonly BaseTarget _baseTarget;
        private readonly IHandlerSetter _handlerSetter;

        public Aim(BaseTarget baseTarget,
            IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData,
            IHandlerSetter handlerSetter) : base(
                mutatorObservable, mutatorData)
        {
            _baseTarget = baseTarget;
            _handlerSetter = handlerSetter;
        }

        protected override void Set()
        {
            _handlerSetter.Set<PlayerAimHandler, IPlayerTargetHandler>();
        }

        protected override void Remove()
        {
            _baseTarget.Set();
        }
    }
}