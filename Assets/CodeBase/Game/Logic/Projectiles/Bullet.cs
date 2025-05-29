using Game.Logic.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Projectiles
{
    public class Bullet : MonoBehaviour, IProjectile
    {
        public event Action<IProjectile, GameObject> InvokeHit;

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

        public virtual void Initialize(Vector2 startPos, Vector2 targetPos)
        {
            transform.position = startPos;
            _direction = (targetPos - startPos).normalized;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            _bulletMove.Move(_direction);
        }

        [Inject]
        protected virtual void Construct(BulletMoveHandler bulletMove)
        {
            _bulletMove = bulletMove;
        }

        protected virtual void OnHit(GameObject objectHit)
        {
            InvokeHit?.Invoke(this, objectHit);
        }

        protected virtual void OnDestroy()
        {
            _bulletMove.OnTrigger -= OnHit;
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, Bullet>, IProjectilePool
        {
            public void DespawnProjectile(IProjectile projectile)
            {
                if (projectile == null || !(projectile is Bullet bullet)) return;
                Despawn(bullet);
            }

            public IProjectile SpawnProjectile(Vector2 startPos, Vector2 target)
                => Spawn(startPos, target);

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