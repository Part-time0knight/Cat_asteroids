using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;

namespace Game.Logic.Player.Mutators.ReactiveArmorMutator
{
    public class ReactiveArmor : AbstractMutator
    {
        private readonly IHandlerGetter _handlerGetter;
        private readonly IHandlerSetter _handlerSetter;

        private IPlayerDamageHandler _damageHandler;

        protected override Mutator Id => Mutator.ReactiveArmor;

        public ReactiveArmor(IHandlerGetter handlerGetter,
            IHandlerSetter handlerSetter,
            IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData) : base(mutatorObservable,
                mutatorData)
        {
            _handlerSetter = handlerSetter;
            _handlerGetter = handlerGetter;
        }

        public override void Initialize()
        {
            base.Initialize();
            _handlerSetter.Set<ReactiveArmorShootHandler, ReactiveArmorShootHandler>();
        }

        protected override void Remove()
        {
            _damageHandler.OnTryTakeDamage -= InvokeTryTakeDamage;
        }

        protected override void Set()
        {
            _damageHandler =
                _handlerGetter.Get<IPlayerDamageHandler>();

            _damageHandler.OnTryTakeDamage += InvokeTryTakeDamage;
        }

        private void InvokeTryTakeDamage()
        {
            _handlerGetter.Get<ReactiveArmorShootHandler>().Shoot();
        }
    }
}