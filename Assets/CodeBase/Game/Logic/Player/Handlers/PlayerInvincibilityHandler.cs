using Game.Logic.Misc;
using System;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class PlayerInvincibilityHandler : IInvincibilityHandler
    {
        public event Action<bool> OnPowerChange;

        private readonly Settings _settings;
        private Timer _timer;

        public PlayerInvincibilityHandler(Settings settings)
        {
            _settings = settings;
            _timer = new();
        }

        public void Start()
        {
            if (_timer.Active)
                _timer.Stop();
            OnPowerChange.Invoke(true);
            _timer.Initialize(_settings.Duration, Stop).Play();
        }

        public void Pause()
        {
            if (!_timer.Active)
                return;
            _timer.Pause();
        }

        public void Continue()
        {
            if (!_timer.Active)
                return;
            _timer.Play();
        }

        public void Stop()
        {
            if (_timer.Active)
                _timer.Stop();
            OnPowerChange?.Invoke(false);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float Duration { get; private set; }
        }
    }
}