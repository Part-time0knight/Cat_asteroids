using Cysharp.Threading.Tasks;
using Game.Logic.Player;
using Game.Logic.Weapon;
using System;
using System.Threading;
using UnityEngine;

namespace Game.Logic.Enemy.Ice
{
    public class IceShootHandler : ShootHandler, IDisposable
    {
        private readonly IPlayerPositionReader _playerPositionReader;
        private readonly IEnemyPositionReader _enemyPositionReader;
        private readonly Transform _transform;
        private readonly IceSettings _iceSettings;

        private CancellationTokenSource _cts = null;

        public IceShootHandler(IPlayerPositionReader playerPositionReader,
            IEnemyPositionReader enemyPositionReader,
            Transform transform,
            IceBullet.IcePool bulletPool,
            IceSettings settings) 
            : base(bulletPool,
                  settings)
        {
            _playerPositionReader = playerPositionReader;
            _enemyPositionReader = enemyPositionReader;
            _transform = transform;
            _iceSettings = settings;
        }

        public void StartAutomatic()
        {
            if (_cts != null)
                return;
            _cts = new();
            _timer.Initialize(
                _settings.AttackDelay).Play();
            Repeater().Forget();
        }

        public void StopAutomatic()
        {
            if (_cts == null)
                return;
            _cts.Cancel();
            _cts = null;
            Clear();
        }

        public void Dispose()
        {
            StopAutomatic();
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active, cancellationToken: _cts.Token);
                Vector2 target = GetTarget();
                if (!_cts.IsCancellationRequested)
                    Shoot(_transform.position, target);
            } while (!_cts.IsCancellationRequested);
        }

        protected override void Hit(Bullet bullet, GameObject target)
        {
            if (target.transform == _transform)
                return;
            base.Hit(bullet, target);
        }

        private Vector2 GetTarget()
        {
            Vector2 enemyPos = _enemyPositionReader.GetNearest(_transform.position);
            float toEnemy = Vector2.Distance(_transform.position, enemyPos);
            float toPlayer = Vector2.Distance(_transform.position, _playerPositionReader.Position);
            bool isPlayer = toEnemy < 0.01f || toEnemy > _iceSettings.SafeZoneDistance || toEnemy > toPlayer;

            if (isPlayer)
                return _playerPositionReader.Position;

            return isPlayer ? _playerPositionReader.Position : enemyPos;
        }


        [Serializable]
        public class IceSettings : Settings
        {
            /// <summary>
            /// If another enemy is within this range, it will be attacked instead of the player.
            /// </summary>
            [field: SerializeField] public float SafeZoneDistance { get; private set; }
        }
    }
}