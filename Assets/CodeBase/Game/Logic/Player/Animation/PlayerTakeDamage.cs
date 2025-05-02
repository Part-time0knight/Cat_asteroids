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
            if (_sequence != null && _sequence.IsPlaying())
                Reset();

            int cycles = Mathf.Max(
                Mathf.FloorToInt(_settings.Length / (_settings.TicLength * 2)),
                1);
            _sequence = DOTween.Sequence();

            for (int i = 0; i < cycles; i++)
            {
                _sequence
                    .Append(_sprite.DOColor(_settings.DamageColor, _settings.TicLength)
                    .SetEase(Ease.OutQuad));
                _sequence
                    .Append(_sprite.DOColor(_baseColor, _settings.TicLength)
                    .SetEase(Ease.OutQuad));
            }

            _sequence.OnComplete(Reset).Play();
            _particleSystem.Play();
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