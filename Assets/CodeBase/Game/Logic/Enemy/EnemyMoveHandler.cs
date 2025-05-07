using Game.Logic.Handlers;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Random = UnityEngine.Random;

namespace Game.Logic.Enemy
{
    public class EnemyMoveHandler : MoveHandler
    {

        private float _randomSpeed = 0;
        private EnemySettings _settings;

        public EnemyMoveHandler(Rigidbody2D body,
            EnemySettings stats) : base(body, stats)
        {
            _settings = stats;
        }

        public void Initialize()
        {
            _randomSpeed = Random.Range(_settings.MinimalSpeed, _settings.Speed);
        }

        public override void Move(Vector2 speedMultiplier)
        {
            if (_randomSpeed == 0)
                _randomSpeed = (_settings.MinimalSpeed + _settings.Speed) * 0.5f;
            _body.AddForce(speedMultiplier * _randomSpeed, ForceMode2D.Impulse);
        }



        [Serializable]
        public class EnemySettings : Settings
        {
            [field: SerializeField] public float MinimalSpeed { get; private set; }
        }
    }
}