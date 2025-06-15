using Game.Logic.Handlers;
using Game.Logic.Misc;
using Game.Logic.Player.Mutators.ShooterMutators;
using System;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class BurstShootHandler : ShootHandler, IPlayerShootHandler, IDisposable
    {
        private readonly IPlayerScoreWriter _scoreWriter;
        private readonly BurstShootSettings _burstSettings;
        private readonly Burst _mutator;
        private readonly Timer _burstTimer = new();
        private readonly IBurstWritter _burstWritter;

        private Func<Vector2> _targetGetter;
        private Func<Vector2> _positionGetter;

        private int _magazine = 0;

        public bool Active
        {
            set
            {
                if (value)
                    StartManual();
                else
                    StopManual();
            }
        }

        public bool IsPause
        {
            set
            {
                if (value)
                    Pause();
                else
                    Continue();
            }
        }

        public BurstShootHandler(ProjectileManager projectileManager,
            BurstShootSettings settings,
            PlayerFacade playerFacade,
            IPlayerScoreWriter scoreWriter,
            Burst mutator, IBurstWritter burstWritter) : base(
                projectileManager,
                playerFacade,
                settings)
        {
            _scoreWriter = scoreWriter;
            _burstSettings = settings;
            _mutator = mutator;
            _burstWritter = burstWritter;
        }

        public void SetTarget(Func<Vector2> targetGetter)
        {
            _targetGetter = targetGetter;
        }

        public void SetPosition(Func<Vector2> positionGetter)
        {
            _positionGetter = positionGetter;
        }

        public override void Initialize()
        {
            base.Initialize();
            _magazine = _burstSettings.BurstMagazine;
            _burstWritter.Ammo = _magazine;
        }

        public void Shoot()
        {
            Vector2 target = _targetGetter.Invoke();
            Vector2 position = _positionGetter.Invoke();
            Shoot(position, target);
        }

        public override void Shoot(Vector2 weaponPos, Vector2 target)
        {
            if (_burstTimer.Active) return;
            if (_magazine == 0) return;
            _magazine--;
            _burstWritter.Ammo = _magazine;
            CreateProjectile(weaponPos, target);

            _burstTimer.Initialize(
                time: _burstSettings.BurstDelay,
                step: _burstSettings.BurstDelay,
                OnEndReload).Play();

            if (!_timer.Active)
                ReloadMagazine();
        }

        public override void Pause()
        {
            base.Pause();
            _burstTimer.Pause();
        }

        public override void Continue()
        {
            base.Continue();
            _burstTimer.Play();
        }

        private void StartManual()
        {
            Debug.Log("Start manual");
            _mutator.OnFire += Shoot;
        }

        private void StopManual()
        {
            Debug.Log("Stop manual");
            _mutator.OnFire -= Shoot;
        }


        private void ReloadMagazine()
        {
            _timer.Initialize(
                time: _settings.AttackDelay,
                step: _settings.AttackDelay,
                callback: AddBulletMagazine)
                .Play();
            _burstWritter.ReloadTime = _settings.AttackDelay;
        }

        private void AddBulletMagazine()
        {
            _magazine++;
            _burstWritter.Ammo = _magazine;
            if (_magazine < _burstSettings.BurstMagazine)
                ReloadMagazine();
        }

        protected override void OnHit(UnitFacade unitHandler)
        {
            base.OnHit(unitHandler);
            if (unitHandler == null)
                return;
            if (unitHandler.Score > 0)
                _scoreWriter.AddScore(unitHandler.Score, unitHandler.transform.position);
        }

        public void Dispose()
        {
            
            Clear();
        }

        [Serializable]
        public class BurstShootSettings : Settings
        {
            [field: SerializeField] public float BurstDelay { get; private set; } = 0.1f;
            [field: SerializeField] public int BurstMagazine { get; private set; } = 3;
        }
    }
}