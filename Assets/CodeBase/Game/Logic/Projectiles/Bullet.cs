using Game.Logic.Handlers;
using Game.Logic.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Projectiles
{
    public class Bullet : UnitFacade, IProjectile
    {
        public event Action<IProjectile, GameObject> OnHit;
        public event Action<IProjectile> OnDead;

        protected Vector2 _direction = Vector2.zero;
        protected BulletMoveHandler _bulletMove;
        protected ProjectileDamageHandler _damageHandler;

        public override bool Pause
        {
            set
            {
                if (value)
                    SetPause();
                else
                    Continue();
            }
        }

        protected virtual void SetPause()
        {
            _bulletMove.Pause();
        }

        protected virtual void Continue()
        {
            _bulletMove.Continue();
        }

        protected virtual void Awake()
        {
            _bulletMove.OnTrigger += InvokeHit;
            _bulletMove.OnCollision += InvokeHit;
            _damageHandler.OnDeath += InvokeDeath;
        }

        public virtual void Initialize(Vector2 startPos, Vector2 targetPos)
        {
            transform.position = startPos;
            _direction = (targetPos - startPos).normalized;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            _damageHandler.Reset();
            _bulletMove.Move(_direction);
        }

        public override void MakeCollision(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        protected virtual void Construct(BulletMoveHandler bulletMove,
            ProjectileDamageHandler projectileDamageHandler)
        {
            _bulletMove = bulletMove;
            _damageHandler = projectileDamageHandler;
        }

        protected virtual void InvokeHit(GameObject objectHit)
        {
            OnHit?.Invoke(this, objectHit);
        }

        protected virtual void OnDestroy()
        {
            _bulletMove.OnTrigger -= InvokeHit;
            _bulletMove.OnCollision -= InvokeHit;
            _damageHandler.OnDeath -= InvokeDeath;
        }

        protected virtual void InvokeDeath()
        {
            OnDead?.Invoke(this);
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, Bullet>, IProjectilePool
        {
            public virtual void DespawnProjectile(IProjectile projectile)
            {
                var bullet = projectile as Bullet;
                if (projectile == null || bullet == null) return;

                bullet.Pause = true;
                Despawn(bullet);
                
            }

            public virtual IProjectile SpawnProjectile(Vector2 startPos, Vector2 target)
            {
                var item = Spawn(startPos, target);
                return item;
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