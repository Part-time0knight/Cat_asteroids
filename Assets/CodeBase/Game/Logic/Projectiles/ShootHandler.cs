using Game.Logic.Handlers;
using Game.Logic.Misc;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Weapon
{
    public abstract class ShootHandler : IInitializable
    {

        protected readonly Bullet.Pool _bulletPool;
        protected readonly Settings _settings;
        protected readonly Timer _timer = new();

        protected List<Bullet> _bullets = new();
        protected Bullet _currentBullet;

        private UnitHandler _unitHandler;

        public ShootHandler(Bullet.Pool bulletPool, Settings settings)
        {
            _bulletPool = bulletPool;
            _settings = settings;

            _settings.CurrentAttackDelay = _settings.AttackDelay;
            _settings.CurrentDamage = _settings.Damage;
            _settings.CanShoot = true;
        }

        public virtual void Initialize()
        {
            _timer.Initialize(
                time: 0f, step: 0f, null)
                .Play();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weponPos">World space position</param>
        /// <param name="target">World space position</param>
        /// <param name="onReloadEnd"></param>
        public virtual void Shoot(Vector2 weponPos, Vector2 target, Action onReloadEnd = null)
        {
            _currentBullet = _bulletPool
                .Spawn(weponPos, target);
            _bullets.Add(_currentBullet);
            _settings.InvokeShot?.Invoke();
            _settings.CanShoot = false;
            _currentBullet.InvokeHit += Hit;
            
            Action onEnd = () =>
            {
                _settings.CanShoot = true;
                onReloadEnd?.Invoke();
            };

            if (_timer.Active)
            {
                _timer.Stop();
                Debug.LogWarning(GetType().Name + " broken timer");
            }

            _timer.Initialize(
                _settings.CurrentAttackDelay, step: 0.05f, onEnd).Play();
        }

        protected virtual void Hit(Bullet bullet, GameObject target)
        {
            _unitHandler = target.GetComponent<UnitHandler>();
            if (target.tag == _settings.Owner)
                return;
            bullet.InvokeHit -= Hit;
            _bulletPool.Despawn(bullet);
            _bullets.Remove(bullet);
            if (_unitHandler)
                _unitHandler.MakeCollizion(_settings.CurrentDamage);
            OnHit(_unitHandler);
        }

        protected virtual void OnHit(UnitHandler unitHandler)
        {

        }

        [Serializable]
        public class Settings
        {
            /// <summary>
            /// Called when handler make a shot. 
            /// </summary>
            public Action InvokeShot;

            [field: SerializeField] public float AttackDelay { get; private set; }
            [field: SerializeField] public int Damage { get; private set; }

            public float CurrentAttackDelay { get; set; }
            
            public int CurrentDamage { get; set; }

            public bool CanShoot { get; set; }

            /// <summary>
            /// Tag of the GameObject that belongs to the owner of the ShootHandler.
            /// </summary>
            public string Owner { get; set; }
        }

    }
}

