using Game.Logic.Handlers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMoveHandler : MoveHandler
    {
        private readonly List<RaycastHit2D> _raycasts;
        public Action<GameObject> InvokeCollision;
        
        public BulletMoveHandler(Rigidbody2D body, BulletSettngs stats, IPauseHandler pauseHandler) : base(body, stats, pauseHandler)
        {
            _raycasts = new();
            _filter.useTriggers = true;
            _collisionOffset = 0f;
        }

        protected override Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _body.Cast(Vector2.zero, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);

            foreach (var hit in _raycasts)
                InvokeCollision?.Invoke(hit.transform.gameObject);

            return speedMultiplier;
        }

        [Serializable]
        public class BulletSettngs : Settings
        { }
    }
}