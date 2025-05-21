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
        private int _rotateDirection = -2;
        private float _speed = -1;

        public AsteroidRotate(SpriteRenderer sprite, Settings settings)
        {
            _spriteRotate = sprite;
            _settings = settings;
        }

        public void Dispose()
        {
            Stop();
        }

        public void Initialize()
        {
            _rotateDirection = Random.Range(0, 2) == 0 ? -1 : 1;
            _speed = Random.Range(_settings.RotationSpeed.x, _settings.RotationSpeed.y);
        }

        public void Play()
        {
            if (_rotationTween != null)
                _rotationTween.Kill();

            if (_speed == -1)
                _speed = (_settings.RotationSpeed.x + _settings.RotationSpeed.y) * 0.5f;

            if (_rotateDirection == -2)
                _rotateDirection = 1;

            _rotationTween = _spriteRotate.transform.DORotate(
                new Vector3(0, 0, 360f * _rotateDirection),
                360f / _speed,
                RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        public void Stop()
        {
            if (_rotationTween != null)
                _rotationTween.Kill();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Vector2 RotationSpeed { get; private set;}
        }
    }
}