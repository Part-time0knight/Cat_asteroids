using Game.Logic.Handlers;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMoveHandler : MoveHandler
    {
        public Action<GameObject> InvokeCollision;
        
        public BulletMoveHandler(Rigidbody2D body, BulletSettngs stats) : base(body, stats)
        {
            body.OnTriggerEnter2DAsObservable().Subscribe(collision => Collision(collision.gameObject));
        }

        private void Collision(GameObject collisionObject)
            => InvokeCollision?.Invoke(collisionObject);

        [Serializable]
        public class BulletSettngs : Settings
        { }
    }
}