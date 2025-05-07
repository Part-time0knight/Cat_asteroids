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
        

        private EnemySettings _settings;

        public EnemyMoveHandler(Rigidbody2D body,
            EnemySettings stats) : base(body, stats)
        {
            _settings = stats;
        }

        public override void Move(Vector2 speedMultiplier)
        {
            _body.AddForce(speedMultiplier * Random.Range(_settings.MinimalSpeed, _settings.Speed), ForceMode2D.Impulse);
        }



        [Serializable]
        public class EnemySettings : Settings
        {
            [field: SerializeField] public float MinimalSpeed { get; private set; }
        }
    }
}