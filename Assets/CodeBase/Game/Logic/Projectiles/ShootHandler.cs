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
        }

        public virtual void Initialize()
        {
            _timer.Initialize(
                time: 0f, step: 0f, null)
                .Play();
        }

        public virtual void SetPause()
        {
            PauseReload();
            foreach (var bullet in _bullets)
                bullet.Pause();
        }

        public virtual void Clear()
        {
            while (_bullets.Count > 0)
            {
                _bullets[0].InvokeHit -= Hit;
                _bulletPool.Despawn(_bullets[0]);
                _bullets.Remove(_bullets[0]);
            }
        }

        public virtual void Continue()
        {
            ContinueReload();
            foreach (var bullet in _bullets)
                bullet.Continue();
        }

        protected virtual void PauseReload()
        {
            if (!_timer.Active)
                return;
            _timer.Pause();
        }

        protected virtual void ContinueReload()
        {
            if (!_timer.Active)
                return;
            _timer.Play();
        }



        /// <param name="weponPos">World space position</param>
        /// <param name="target">World space position</param>
        /// <param name="onReloadEnd"></param>
        public virtual void Shoot(Vector2 weponPos, Vector2 target, Action onReloadEnd = null)
        {
            _currentBullet = _bulletPool
                .Spawn(weponPos, target);
            _bullets.Add(_currentBullet);
            _currentBullet.InvokeHit += Hit;
            
            Action onEnd = () =>
            {
                OnEndReload();
                onReloadEnd?.Invoke();
            };

            if (_timer.Active)
            {
                _timer.Stop();
                Debug.LogWarning(GetType().Name + " broken timer");
            }

            _timer.Initialize(
                _settings.AttackDelay, step: 0.05f, onEnd).Play();
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
                _unitHandler.MakeCollision(_settings.Damage);
            OnHit(_unitHandler);
        }

        protected virtual void OnEndReload()
        { }

        protected virtual void OnHit(UnitHandler unitHandler)
        { }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float AttackDelay { get; private set; }
            [field: SerializeField] public int Damage { get; private set; }

            /// <summary>
            /// Tag of the GameObject that belongs to the owner of the ShootHandler.
            /// </summary>
            public string Owner { get; set; }
        }

    }
}

