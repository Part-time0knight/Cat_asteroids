using Cysharp.Threading.Tasks;
using Game.Logic.Enemy.Ice.IceM;
using Game.Logic.Handlers;
using Game.Logic.Player;
using Game.Logic.Projectiles;
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
            EnemyFacade iceFacade,
            Transform transform,
            ProjectileManager projectileManager,
            IceSettings settings) 
            : base(projectileManager,
                  iceFacade,
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
        }

        public void Dispose()
        {
            StopAutomatic();
            Clear();
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active, cancellationToken: _cts.Token);
                Vector2 target = GetTarget();
                Vector2 startPos = new(_transform.position.x, _transform.position.y);
                startPos = startPos
                    + (target - startPos).normalized 
                    * 1.3f;
                if (!_cts.IsCancellationRequested)
                    Shoot(startPos, target);
            } while (!_cts.IsCancellationRequested);
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