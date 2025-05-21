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
        protected BulletMoveHandler _bulletMove;

        public virtual void Pause()
        {
            _bulletMove.Pause();
        }

        public virtual void Continue()
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
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
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