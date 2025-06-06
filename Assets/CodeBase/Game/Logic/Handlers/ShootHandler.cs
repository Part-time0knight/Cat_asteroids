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
        protected readonly Timer _damageDelay = new();

        protected readonly List<IProjectile> _bullets = new();
        protected readonly Dictionary<IProjectile, IProjectilePool> _bulletsPools = new();


        private UnitFacade _unitHandler;

        public ShootHandler(ProjectileManager projectileManager, 
            UnitFacade unitFacade,
            Settings settings)
        {
            _projectileManager = projectileManager;
            _settings = settings;
            _settings.Owner = unitFacade.gameObject;
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
                bullet.Pause = true;
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
                bullet.Pause = false;
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
        public virtual void Shoot(Vector2 weaponPos, Vector2 target)
        {
            CreateProjectile(weaponPos, target);

            if (_timer.Active)
            {
                _timer.Stop();
                Debug.LogWarning(GetType().Name + " broken timer");
            }

            _timer.Initialize(
                time: _settings.AttackDelay,
                step: _settings.AttackDelay,
                OnEndReload).Play();
        }

        protected virtual void Hit(IProjectile iBullet, GameObject target)
        {
            _unitHandler = target.GetComponent<UnitFacade>();

            if (target.transform == _settings.Owner.transform)
                return;
            if (target.tag == "Border")
            {
                RemoveProjectile(iBullet);
                return;
            }

            if (_unitHandler != null)
            {
                MakeDamage(_unitHandler);
            }
            
            OnHit(_unitHandler);
        }

        protected void MakeDamage(UnitFacade unit)
        {
            Timer timer = new();
            timer.Initialize(Time.fixedDeltaTime,
                    () => unit.MakeCollision(_settings.Damage))
                    .Play();
        }

        protected IProjectile CreateProjectile(Vector2 weaponPos, Vector2 target)
        {
            var currentBullet = _projectileManager.Pool
                .SpawnProjectile(weaponPos, target);
            _bullets.Add(currentBullet);
            _bulletsPools.Add(currentBullet, _projectileManager.Pool);
            currentBullet.OnHit += Hit;
            currentBullet.OnDead += RemoveProjectile;
            return currentBullet;
        }

        protected void RemoveProjectile(IProjectile iBullet)
        {
            iBullet.OnHit -= Hit;
            iBullet.OnDead -= RemoveProjectile;
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
            public GameObject Owner { get; set; }
        }

    }
}

