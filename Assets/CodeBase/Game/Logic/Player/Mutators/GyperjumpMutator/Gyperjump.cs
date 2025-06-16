using Core.MVVM.Windows;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Misc;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using Game.Presentation.View;
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Logic.Player.Mutators.GyperjumpMutator
{
    public class Gyperjump : AbstractMutator
    {
        protected override Mutator Id => Mutator.Gyperjump;
        private readonly BundleInput _input;
        private readonly IHandlerGetter _handlerGetter;
        private readonly Rigidbody2D _body;
        private readonly Timer _timer = new();
        private readonly Settings _settings;
        private readonly IGyperjumpWriter _writer;
        private readonly IWindowResolve _windowResolve;
        private readonly IWindowFsm _windowFsm;

        public Gyperjump(IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData,
            BundleInput input,
            IHandlerGetter handlerGetter,
            Rigidbody2D body,
            IGyperjumpWriter gyperjumpWriter,
            IWindowResolve windowResolve,
            IWindowFsm windowFsm,
            Settings settings) : base(mutatorObservable,
                mutatorData)
        {
            _input = input;
            _handlerGetter = handlerGetter;
            _body = body;
            _settings = settings;
            _writer = gyperjumpWriter;
            _windowResolve = windowResolve;
            _windowFsm = windowFsm;
        }

        public override void Initialize()
        {
            base.Initialize();
            _windowResolve.Set<GyperjumpView>();
        }

        protected override void Remove()
        {
            _input.OnButtonDown -= InvokeJump;
            _windowFsm.CloseWindow(typeof(GyperjumpView));
        }

        protected override void Set()
        {
            _input.OnButtonDown += InvokeJump;
            _writer.ReloadTime = _settings.Delay;
            _windowFsm.OpenWindow(typeof(GyperjumpView), false);
        }

        protected override void InvokePause(bool pause)
        {
            base.InvokePause(pause);
            _writer.Pause = pause;
            if (pause)
                _timer.Pause();
            else
                _timer.Play();
        }

        private void InvokeJump(int id)
        {
            if (id != (int)Id) return;
            if (_pause) return;
            if (_timer.Active) return;
            
            _handlerGetter.Get<IInvincibilityHandler>().Start();
            Vector2 newPos = 
                new(Random.Range(-_settings.WidthBorder, _settings.WidthBorder),
                    Random.Range(-_settings.HeightBorder, _settings.HeightBorder));
            _body.transform.position = newPos;

            _timer
                .Initialize(_settings.Delay,
                    _settings.Delay,
                    OnEndReload)
                .Play();

            _writer.Reload = true;
            _writer.SetReload();
        }

        private void OnEndReload()
        {
            _writer.Reload = false;
            _writer.SetEndReload();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float Delay { get; private set; } = 5.0f;
            [field: SerializeField] public float HeightBorder { get; private set; } = 14.0f;
            [field: SerializeField] public float WidthBorder { get; private set; } = 25.0f;
        }
    }
}