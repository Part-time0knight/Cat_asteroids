using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Logic.Enemy.Asteroid
{
    public class AsteroidRotate : IDisposable
    {
        private readonly SpriteRenderer _spriteRotate;
        private readonly Settings _settings;

        private Tween _rotationTween;

        public AsteroidRotate(SpriteRenderer sprite, Settings settings)
        {
            _spriteRotate = sprite;
            _settings = settings;
        }

        public void Dispose()
        {
            if (_rotationTween != null)
                _rotationTween.Kill();
        }

        public void Play()
        {
            if (_rotationTween != null && _rotationTween.IsPlaying())
                return;

            int direction = Random.Range(0, 2) == 0 ? -1 : 1;
            float speed = Random.Range(_settings.RotationSpeed.x, _settings.RotationSpeed.y);

            _rotationTween = _spriteRotate.transform.DORotate(
                new Vector3(0, 0, 360f * direction),
                360f / speed,
                RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        public void Stop()
        {
            if (_rotationTween != null && _rotationTween.IsPlaying())
                _rotationTween.Kill();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Vector2 RotationSpeed { get; private set;}
        }
    }
}