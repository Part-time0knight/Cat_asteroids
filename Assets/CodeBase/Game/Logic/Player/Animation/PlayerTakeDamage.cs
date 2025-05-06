using DG.Tweening;
using System;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class PlayerTakeDamage : IDisposable
    {
        private readonly SpriteRenderer _sprite;
        private readonly Settings _settings;
        private readonly Color _baseColor;
        private readonly ParticleSystem _particleSystem;

        private Sequence _sequence;


        public PlayerTakeDamage(SpriteRenderer spriteRenderer,
            ParticleSystem particleSystem,
            Settings settings)
        {
            _sprite = spriteRenderer;
            _settings = settings;
            _baseColor = _sprite.color;
            _particleSystem = particleSystem;
        }

        public void Dispose()
        {
            Reset();
        }

        public void Play()
        {
            if (_sequence != null)
                Reset();

            int cycles = Mathf.Max(
                Mathf.FloorToInt(_settings.Length / (_settings.TicLength * 2)),
                1);

            _sequence = DOTween.Sequence();

            _sequence
                .Append(_sprite
                    .DOColor(_settings.DamageColor, _settings.TicLength)
                    .SetEase(Ease.OutQuad))
                .Append(_sprite
                    .DOColor(_baseColor, _settings.TicLength)
                    .SetEase(Ease.OutQuad))
                .SetLoops(cycles)
                .OnComplete(Reset)
                .Play();

            _particleSystem.Play();
        }

        public void Pause()
        {
            if (_sequence == null || !_sequence.IsActive())
                return;
            _sequence.Pause();
        }

        public void Continue()
        {
            if (_sequence == null || !_sequence.IsActive())
                return;
            _sequence.Play();
        }

        private void Reset()
        {
            if (_sequence != null)
                _sequence.Kill();
            _particleSystem.Stop();
            _sprite.color = _baseColor;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Color DamageColor { get; private set; }
            [field: SerializeField] public float Length { get; private set; }
            [field: SerializeField] public float TicLength { get; private set; }
        }
    }
}