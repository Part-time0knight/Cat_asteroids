using Game.Logic.Handlers.Strategy;
using Game.Logic.Misc;
using Game.Logic.Player.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Logic.Player.Mutators.GyperjumpMutator
{
    public class Gyperjump : AbstractMutator
    {
        private const float HeightBorder = 14.0f;
        private const float WidthBorder = 25.0f;

        protected override Mutator Id => Mutator.Gyperjump;
        private readonly BundleInput _input;
        private readonly IHandlerGetter _handlerGetter;
        private readonly Rigidbody2D _body;
        private readonly Timer _timer = new();
        private readonly Settings _settings;


        public Gyperjump(IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData,
            BundleInput input,
            IHandlerGetter handlerGetter,
            Rigidbody2D body,
            Settings settings) : base(mutatorObservable,
                mutatorData)
        {
            _input = input;
            _handlerGetter = handlerGetter;
            _body = body;
            _settings = settings;
        }

        protected override void Remove()
        {
            _input.OnButtonDown -= InvokeJump;
        }

        protected override void Set()
        {
            _input.OnButtonDown += InvokeJump;
        }

        private void InvokeJump(int id)
        {
            if (id != (int)Id) return;
            if (_timer.Active) return;

            _handlerGetter.Get<IInvincibilityHandler>().Start();
            Vector2 newPos = 
                new(Random.Range(-WidthBorder, WidthBorder),
                    Random.Range(-HeightBorder, HeightBorder));
            _body.transform.position = newPos;
            _timer.Initialize(_settings.Delay, _settings.Delay, null).Play();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float Delay { get; private set; } = 5.0f;
        }
    }
}