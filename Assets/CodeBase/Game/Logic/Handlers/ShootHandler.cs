using Game.Logic.Enemy.Ice;
using Game.Logic.Misc;
using Game.Logic.Projectiles;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Handlers
{
    public abstract class ShootHandler : IInitializable, IShootHandler
    {

        protected readonly ProjectileManager _projectileManager;
        protected readonly Settings _settings;
        protected readonly Timer _timer = new();

        protected readonly List<IProjectile> _bullets = new();
        protected readonly Dictionary<IProjectile, IProjectilePool> _bulletsPools = new();

        protected IProjectile _currentBullet;

        private UnitFacade _unitHandler;

        public ShootHandler(ProjectileManager projectileManager, Settings settings)
        {
            _projectileManager = projectileManager;
            _settings = settings;
        }

        public virtual void Initialize()
        {
            _timer.Initialize(
                time: 0f, step: 0f, null)
                .Play();
        }

        public virtual void Pause()
        {
            PauseReload();
            foreach (var bullet in _bullets)
                bullet.Pause();
        }

        public virtual void Clear()
        {
            while (_bullets.Count > 0)
            {
                RemoveProjectile(_bullets[0]);
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



        /// <param name="weaponPos">World space position</param>
        /// <param name="target">World space position</param>
        /// <param name="onReloadEnd"></param>
        public virtual void Shoot(Vector2 weaponPos, Vector2 target, Action onReloadEnd = null)
        {
            _currentBullet = _projectileManager.Pool
                .SpawnProjectile(weaponPos, target);
            _bullets.Add(_currentBullet);
            _bulletsPools.Add(_currentBullet, _projectileManager.Pool);
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

        protected virtual void Hit(IProjectile iBullet, GameObject target)
        {
            _unitHandler = target.GetComponent<UnitFacade>();

            if (target.tag == _settings.Owner)
                return;
            RemoveProjectile(iBullet);
            if (_unitHandler)
                _unitHandler.MakeCollision(_settings.Damage);
            OnHit(_unitHandler);
        }

        private void RemoveProjectile(IProjectile iBullet)
        {
            iBullet.InvokeHit -= Hit;
            _bulletsPools[iBullet].DespawnProjectile(iBullet);
            _bulletsPools.Remove(iBullet);
            _bullets.Remove(iBullet);
        }

        protected virtual void OnEndReload()
        { }

        protected virtual void OnHit(UnitFacade unitHandler)
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

