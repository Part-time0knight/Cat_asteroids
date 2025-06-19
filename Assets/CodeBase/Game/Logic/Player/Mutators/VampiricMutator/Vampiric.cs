using Core.MVVM.Windows;
using Game.Logic.Handlers;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using Game.Presentation.View;
using System;
using UnityEngine;

namespace Game.Logic.Player.Mutators.VampiricMutator
{
    public class Vampiric : AbstractMutator
    {
        private readonly IHandlerGetter _handlerGetter;
        private readonly IPlayerScoreReader _scoreReader;
        private readonly IPlayerHitsReader _playerHitsReader;
        private readonly IWindowFsm _windowFsm;
        private readonly IWindowResolve _windowResolve;
        private readonly IVampiricWriter _vampiricWriter;
        private readonly Settings _settings;

        private int _currentKill = 0;

        protected override Mutator Id => Mutator.Vampiric;

        public Vampiric(IPlayerHitsReader playerHitsReader,
            IPlayerScoreReader playerScoreReader,
            IHandlerGetter handlerGetter,
            IWindowResolve windowResolve,
            IWindowFsm windowFsm,
            IVampiricWriter vampiricWriter,
            Settings settings,
            IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData)
            : base(mutatorObservable,
                mutatorData)
        {
            _playerHitsReader = playerHitsReader;
            _handlerGetter = handlerGetter;
            _scoreReader = playerScoreReader;
            _windowFsm = windowFsm;
            _windowResolve = windowResolve;
            _vampiricWriter = vampiricWriter;
            _settings = settings;
        }

        public override void Initialize()
        {
            base.Initialize();
            _windowResolve.Set<VampiricView>();
        }

        protected override void Remove()
        {
            _scoreReader.OnScoreUpdate -= InvokeKill;
            _windowFsm.CloseWindow(typeof(VampiricView));
        }

        protected override void Set()
        {
            _scoreReader.OnScoreUpdate += InvokeKill;

            _currentKill = 0;
            _vampiricWriter.Reload = true;
            _vampiricWriter.CurrentPoints = _currentKill;
            _vampiricWriter.NeedPoints = _settings.ShieldResetCount;

            _windowFsm.OpenWindow(typeof(VampiricView), false);
            
        }

        private void InvokeKill()
        {
            if (_playerHitsReader.ShieldHits > 0)
            {
                _vampiricWriter.Reload = false;
                return;
            }

            _currentKill++;

            if (_currentKill < _settings.ShieldResetCount) 
            {
                _vampiricWriter.CurrentPoints = _currentKill;
                _vampiricWriter.Reload = true;
                return;
            }

            _vampiricWriter.Reload = false;
            _currentKill = 0;
            _handlerGetter.Get<IPlayerDamageHandler>().SetShield(1);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int ShieldResetCount { get; set; } = 20;
        }
    }
}