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


        protected readonly Rigidbody2D _body;
        protected readonly Settings _stats;
        protected Vector2 _pausedLinearVelocity = Vector2.zero;
        protected float _pausedAngularVelocity = 0f;


        protected Vector2 Velocity
        {
            get => _body.linearVelocity;
            set => _body.linearVelocity = value;
        }

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
        {
            _body.linearVelocity = Vector2.zero;
            _body.angularVelocity = 0;
        }

        public void Pause()
        {
            _pausedLinearVelocity = _body.linearVelocity;
            _pausedAngularVelocity = _body.angularVelocity;
            _body.linearVelocity = Vector2.zero;
            _body.angularVelocity = 0;
        }

        public void Continue()
        {
            _body.linearVelocity = _pausedLinearVelocity;
            _body.angularVelocity = _pausedAngularVelocity;
            _pausedLinearVelocity = Vector2.zero;
            _pausedAngularVelocity = 0;
        }

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