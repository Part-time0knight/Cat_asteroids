using Game.Logic.Misc;
using Game.Logic.Player;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Effects.Particles
{
    public class PlayerHasteEffectHandler
    {
        private readonly Settings _settings;
        private readonly PlayerBaseMoveHandler.PlayerSettings _moveSettings;

        private readonly Timer _timer;

        private ParticleSystem.EmissionModule _emission;

        public PlayerHasteEffectHandler(
            ParticleSystem particleSystem,
            Settings settings,
            PlayerBaseMoveHandler.PlayerSettings moveSettings)
        {
            _emission = particleSystem.emission;
            _settings = settings;
            _moveSettings = moveSettings;
            _timer = new();
        }

        public void InvokeHaste(float speed)
        {
            if (_timer.Active)
                _timer.Stop();
            float emissionRate = _settings.EmissionMin
                + Mathf.InverseLerp(0, _moveSettings.MaxSpeed, speed)
                * _settings.EmissionRate;
            _emission.rateOverTime = emissionRate;
            _timer.Initialize(Time.fixedDeltaTime * 2, Time.fixedDeltaTime, StopHaste).Play();
        }

        private void StopHaste()
        {
            _emission.rateOverTime = 0;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float EmissionMin { get; private set; }
            [field: SerializeField] public float EmissionRate { get; private set; }
        }
    }
}