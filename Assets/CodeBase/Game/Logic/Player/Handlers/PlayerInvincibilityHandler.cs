using Game.Logic.Misc;
using Game.Logic.Player.Animation;
using System;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class PlayerInvincibilityHandler : IInvincibilityHandler
    {
        public event Action<bool> OnPowerChange;

        private readonly Settings _settings;
        private readonly Invincibility _invincibility;

        private Timer _timer;

        public PlayerInvincibilityHandler(Settings settings,
            Invincibility invincibility)
        {
            _invincibility = invincibility;
            _settings = settings;
            _timer = new();
        }

        public void Start(float duration)
        {
            if (_timer.Active)
                _timer.Stop();
            OnPowerChange.Invoke(true);

            _invincibility.Play(duration);
            _timer.Initialize(duration, Stop).Play();
        }

        public void Start()
        {
            Start(_settings.Duration);
        }

        public void Pause()
        {
            if (!_timer.Active)
                return;
            _timer.Pause();
            _invincibility.Pause();
        }

        public void Continue()
        {
            if (!_timer.Active)
                return;
            _timer.Play();
            _invincibility.Continue();
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