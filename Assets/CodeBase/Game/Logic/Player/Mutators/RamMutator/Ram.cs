using Core.MVVM.Windows;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Misc;
using Game.Logic.Player.Animation;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using Game.Presentation.View;
using System;
using UnityEngine;

namespace Game.Logic.Player.Mutators.RamMutator
{
    public class Ram : AbstractMutator
    {
        private readonly BundleInput _input;
        private readonly IHandlerSetter _handlerSetter;
        private readonly IHandlerGetter _handlerGetter;
        private readonly ColorChanger _colorChanger;
        private readonly IRamWriter _writer;
        private readonly IWindowResolve _windowResolve;
        private readonly IWindowFsm _windowFsm;
        private readonly Settings _settings;

        private readonly Timer _timer = new();
        private readonly Timer _colorTimer = new();

        protected override Mutator Id => Mutator.Ram;

        public Ram(IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData,
            BundleInput input,
            IHandlerSetter handlerSetter,
            IHandlerGetter handlerGetter,
            ColorChanger colorChanger,
            IWindowResolve windowResolve,
            IWindowFsm windowFsm,
            IRamWriter writer,
            Settings settings) : base(mutatorObservable,
                mutatorData)
        {
            _input = input;
            _handlerSetter = handlerSetter;
            _handlerGetter = handlerGetter;
            _colorChanger = colorChanger;
            _windowResolve = windowResolve;
            _windowFsm = windowFsm;
            _writer = writer;
            _settings = settings;
        }

        public override void Initialize()
        {
            base.Initialize();
            _windowResolve.Set<RamView>();
        }

        protected override void Set()
        {
            _handlerSetter.Set<RamMoveHandler, IPlayerMoveHandler>();
            _input.OnButtonDown += InvokeRam;
            _writer.ReloadTime = _settings.Reload;
            _windowFsm.OpenWindow(typeof(RamView), false);
        }

        protected override void Remove()
        {
            _input.OnButtonDown -= InvokeRam;
            _handlerSetter.Set<PlayerBaseMoveHandler, IPlayerMoveHandler>();
            _windowFsm.CloseWindow(typeof(RamView));
        }

        protected override void InvokePause(bool pause)
        {
            base.InvokePause(pause);

            _writer.Pause = pause;

            if (pause)
            {
                _timer.Pause();
                _colorTimer.Pause();
            }
            else
            {
                _timer.Play();
                _colorTimer.Play();
            }
        }

        private void InvokeRam(int id)
        {
            if (id != (int)Id) return;
            if (_timer.Active) return;
            if (_pause) return;

            var move = _handlerGetter.Get<IPlayerMoveHandler>() as RamMoveHandler;
            move.Ram(_settings.Speed, _settings.HasteDuration, _settings.Mass);

            _colorChanger.Change(_settings.Color);

            _handlerGetter.Get<IInvincibilityHandler>()
                .Start(_settings.Invincibility);

            _colorTimer
                .Initialize(_settings.HasteDuration, _colorChanger.Reset)
                .Play();

            _timer
                .Initialize(_settings.Reload, InvokeReloadEnd)
                .Play();

            _writer.Reload = true;
        }

        private void InvokeReloadEnd()
        {
            _writer.Reload = false;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float Speed { get; private set; } = 400f;
            [field: SerializeField] public float Mass { get; private set; } = 5f;
            [field: SerializeField] public float HasteDuration { get; private set; } = 2f;
            [field: SerializeField] public float Invincibility { get; private set; } = 2.5f;
            [field: SerializeField] public float Reload { get; private set; } = 7f;
            [field: SerializeField] public Color Color { get; private set; } = Color.red;
        }
    }
}