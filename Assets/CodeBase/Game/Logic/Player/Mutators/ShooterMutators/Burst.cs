using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using System;

namespace Game.Logic.Player.Mutators.ShooterMutators
{
    public class Burst : AbstractMutator
    {
        public event Action OnFire;

        private readonly BaseShooter _baseShooter;
        private readonly IHandlerSetter _handlerSetter;
        private readonly BundleInput _input;

        protected override Mutator Id => Mutator.Burst;

        public Burst(BaseShooter baseShooter,
            IHandlerSetter handlerSetter,
            IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData,
            BundleInput input) : base(mutatorObservable, mutatorData)
        {
            _baseShooter = baseShooter;
            _handlerSetter = handlerSetter;
            _input = input;
        }

        protected override void Remove()
        {
            _baseShooter.Set();
        }

        protected override void Set()
        {
            _handlerSetter.Set<BurstShootHandler, IPlayerShootHandler>();
            _input.OnButton += InvokeFire;
        }

        private void InvokeFire(int id)
        {
            if (id != (int)Id) return;
            OnFire?.Invoke();
        }
    }
}