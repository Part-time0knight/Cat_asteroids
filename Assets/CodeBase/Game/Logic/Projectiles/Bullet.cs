using Game.Logic.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Weapon
{
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet, GameObject> InvokeHit;

        protected Vector2 _direction = Vector2.zero;
        protected Vector2 _sampleDirection = Vector2.zero;
        protected BulletMoveHandler _bulletMove;

        public void Pause()
        {
            _bulletMove.Pause();
        }

        public void Continue()
        {
            _bulletMove.Continue();
        }

        protected virtual void Awake()
        {
            _bulletMove.OnTrigger += OnHit;
        }

        protected virtual void Initialize(Vector2 startPos, Vector2 targetPos)
        {
            transform.position = startPos;
            _direction = (targetPos - startPos).normalized;
            _sampleDirection = _direction;
            _bulletMove.Move(_direction);
        }

        [Inject]
        private void Construct(BulletMoveHandler bulletMove)
        {
            _bulletMove = bulletMove;
        }

        private void OnHit(GameObject objectHit)
        {
            InvokeHit?.Invoke(this, objectHit);
        }

        private void OnDestroy()
        {
            _bulletMove.OnTrigger -= OnHit;
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, Bullet>
        {
            /// <param name="startPos">World space position</param>
            /// <param name="targetPos">World space position</param>
            protected override void Reinitialize(Vector2 startPos, Vector2 targetPos, Bullet item)
            {
                base.Reinitialize(startPos, targetPos, item);
                item.Initialize(startPos, targetPos);
            }
        }
    }
}