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

        protected virtual void Awake()
        {
            _bulletMove.InvokeCollision += OnHit;
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
            _bulletMove.InvokeCollision -= OnHit;
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, Bullet>
        {

            protected Transform _buffer;

            [Inject]
            private void Construct(BulletBuffer buffer)
            {
                _buffer = buffer.transform;
            }

            protected override void OnCreated(Bullet item)
            {
                item.transform.SetParent(_buffer);
                base.OnCreated(item);
            }

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