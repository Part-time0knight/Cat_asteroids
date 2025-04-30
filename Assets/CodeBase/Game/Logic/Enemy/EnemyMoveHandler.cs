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
        public Action<GameObject> InvokeCollision;
        public Action<GameObject> InvokeTrigger;

        private EnemySettings _settings;

        public Vector2 Direction { get; set; }

        public EnemyMoveHandler(Rigidbody2D body,
            EnemySettings stats) : base(body, stats)
        {
            _settings = stats;
            body.OnCollisionEnter2DAsObservable().Subscribe(Collision);
            body.OnTriggerEnter2DAsObservable().Subscribe(Trigger);
        }

        public void Move()
            => Move(Direction);

        public override void Move(Vector2 speedMultiplier)
        {
            _body.AddForce(speedMultiplier * Random.Range(_settings.MinimalSpeed, _settings.Speed), ForceMode2D.Impulse);
        }

        private void Collision(Collision2D collisionObject)
            => InvokeCollision?.Invoke(collisionObject.gameObject);

        private void Trigger(Collider2D triggerObject)
            => InvokeTrigger?.Invoke(triggerObject.gameObject);

        [Serializable]
        public class EnemySettings : Settings
        {
            [field: SerializeField] public float MinimalSpeed { get; private set; }

            public EnemySettings(Settings settings) : base(settings)
            { 
            
            }
        }
    }
}