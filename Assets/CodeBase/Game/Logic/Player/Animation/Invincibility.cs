using DG.Tweening;
using Game.Logic.Misc;
using System;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class Invincibility
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Timer _timer = new();
        private readonly Settings _settings;

        private Sequence _sequence;

        public Invincibility(SpriteRenderer spriteRenderer,
            Settings settings)
        {
            _spriteRenderer = spriteRenderer;
            _settings = settings;
        }

        public void Play(float duration)
        {
            if (_sequence != null)
                Clear();

            _sequence = DOTween.Sequence();

            _sequence
                .Join(_spriteRenderer.transform
                    .DORotate(new Vector3(0, 0, 360), 
                        360f / _settings.RotateSpeed,
                        RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(int.MaxValue, LoopType.Restart))
                .Join(_spriteRenderer
                    .DOFade(0.75f, _settings.BlinkDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(int.MaxValue, LoopType.Yoyo))
                .Play();

            _timer
                .Initialize(duration, duration, Clear)
                .Play();
        }

        public void Pause()
        {
            if (_sequence == null) return;

            _sequence.Pause();
        }

        public void Continue()
        {
            if (_sequence == null) return;

            _sequence.Play();
        }

        public void Clear()
        {
            _spriteRenderer.color = new(1, 1, 1, 0);
            _spriteRenderer.transform.localEulerAngles = Vector3.zero;
            
            if (_timer.Active)
                _timer.Stop();

            _sequence?.Kill();
            _sequence = null;

        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float RotateSpeed { get; private set; } = 120f;
            [field: SerializeField] public float BlinkDuration { get; private set; } = 0.15f;
        }
    }
}