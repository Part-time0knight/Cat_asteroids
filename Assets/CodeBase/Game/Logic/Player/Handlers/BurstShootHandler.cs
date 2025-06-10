using Game.Logic.Handlers;
using Game.Logic.Misc;
using Game.Logic.Player.Mutators.ShooterMutators;
using System;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class BurstShootHandler : ShootHandler, IPlayerShootHandler, IDisposable
    {
        private readonly Transform _weapon;
        private readonly IPlayerScoreWriter _scoreWriter;
        private readonly BurstShootSettings _burstSettings;
        private readonly Burst _mutator;
        private readonly Timer _burstTimer = new();
        private readonly IBurstWritter _burstWritter;

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
            Transform weaponPoint,
            PlayerFacade playerFacade,
            IPlayerScoreWriter scoreWriter,
            Burst mutator, IBurstWritter burstWritter) : base(
                projectileManager,
                playerFacade,
                settings)
        {
            _weapon = weaponPoint;
            _scoreWriter = scoreWriter;
            _burstSettings = settings;
            _mutator = mutator;
            _burstWritter = burstWritter;
        }

        public override void Initialize()
        {
            base.Initialize();
            _magazine = _burstSettings.BurstMagazine;
            _burstWritter.Ammo = _magazine;
        }

        public void Shoot()
        {
            Vector2 target = _weapon.TransformPoint(
                    new(_weapon.localPosition.x, _weapon.localPosition.y + 1f));
            Shoot(_weapon.position, target);
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

        private void StartManual()
        {
            _mutator.OnFire += Shoot;
        }

        private void StopManual()
        {
            _mutator.OnFire -= Shoot;
        }

        public override void Pause()
        {
            base.Pause();
            StopManual();
            _burstTimer.Pause();
        }

        public override void Continue()
        {
            base.Continue();
            StartManual();
            _burstTimer.Play();
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