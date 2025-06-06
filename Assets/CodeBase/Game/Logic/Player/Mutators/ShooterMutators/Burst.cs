using Core.MVVM.Windows;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using Game.Presentation.View;
using System;

namespace Game.Logic.Player.Mutators.ShooterMutators
{
    public class Burst : AbstractMutator
    {
        public event Action OnFire;

        private readonly BaseShooter _baseShooter;
        private readonly IHandlerSetter _handlerSetter;
        private readonly BundleInput _input;
        private readonly IWindowResolve _windowResolve;
        private readonly IWindowFsm _windowFsm;

        private bool _active = false;

        protected override Mutator Id => Mutator.Burst;

        public Burst(BaseShooter baseShooter,
            IHandlerSetter handlerSetter,
            IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData,
            BundleInput input, 
            IWindowResolve windowResolve, 
            IWindowFsm windowFsm) : base(mutatorObservable, mutatorData)
        {
            _baseShooter = baseShooter;
            _handlerSetter = handlerSetter;
            _input = input;
            _windowResolve = windowResolve;
            _windowFsm = windowFsm;
        }

        public override void Initialize()
        {
            base.Initialize();
            _windowResolve.Set<BurstView>();
        }

        protected override void Remove()
        {
            if (!_active) return;
            _active = false;
            _baseShooter.Set();
            _windowFsm.CloseWindow(typeof(BurstView));
        }

        protected override void Set()
        {
            if (_active) return;
            _active = true;
            _handlerSetter.Set<BurstShootHandler, IPlayerShootHandler>();
            _input.OnButton += InvokeFire;
            _windowFsm.OpenWindow(typeof(BurstView), false);
        }

        private void InvokeFire(int id)
        {
            if (id != (int)Id) return;
            OnFire?.Invoke();
        }
    }
}