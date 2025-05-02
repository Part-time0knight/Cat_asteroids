using Game.Logic.Misc;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerInvincibilityHandler
    {
        private readonly Settings _settings;
        private readonly PlayerDamageHandler _playerDamageHandler;
        private Timer _timer;

        public PlayerInvincibilityHandler(Settings settings, PlayerDamageHandler playerDamageHandler)
        {
            _playerDamageHandler = playerDamageHandler;
            _settings = settings;
            _timer = new();
        }

        public void Start()
        {
            if (_timer.Active)
                _timer.Stop();
            _playerDamageHandler.Pause = true;
            _timer.Initialize(_settings.Duration, Stop).Play();
        }

        public void Stop()
        {
            if (_timer.Active)
                _timer.Stop();
            _playerDamageHandler.Pause = false;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float Duration { get; private set; }
        }
    }
}