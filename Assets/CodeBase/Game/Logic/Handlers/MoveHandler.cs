using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Logic.Handlers
{
    public class MoveHandler
    {
        public Action<GameObject> OnTrigger;
        public Action<GameObject> OnCollision;

        protected Vector2 Velocity 
        {
            get => _body.linearVelocity;
            set => _body.linearVelocity = value;
        }

        protected readonly Rigidbody2D _body;
        protected readonly Settings _stats;


        public MoveHandler(Rigidbody2D body, Settings stats)
        {
            _body = body;
            _stats = stats;
            body.OnTriggerEnter2DAsObservable().Subscribe(InvokeTrigger);
            body.OnCollisionEnter2DAsObservable().Subscribe(InvokeCollision);
        }

        public virtual void Move(Vector2 speedMultiplier)
        {
            _body.AddForce(speedMultiplier * _stats.Speed, ForceMode2D.Impulse);
        }


        public void Stop()
            => _body.linearVelocity = Vector2.zero;

        private void InvokeTrigger(Collider2D collision)
            => OnTrigger?.Invoke(collision.gameObject);

        private void InvokeCollision(Collision2D collisionObject)
            => OnCollision?.Invoke(collisionObject.gameObject);

        public class Settings
        {
            [field: SerializeField] public float Speed { get; protected set; }
        }
    }
}