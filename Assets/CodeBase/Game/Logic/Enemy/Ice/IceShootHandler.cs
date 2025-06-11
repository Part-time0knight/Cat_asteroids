using Cysharp.Threading.Tasks;
using Game.Logic.Handlers;
using Game.Logic.Player;
using System;
using UnityEngine;

namespace Game.Logic.Enemy.Ice
{
    public class IceShootHandler : ShootHandler, IDisposable
    {
        private readonly IPlayerPositionReader _playerPositionReader;
        private readonly IEnemyPositionReader _enemyPositionReader;
        private readonly Transform _transform;
        private readonly IceSettings _iceSettings;

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
            Vector2 target, startPos;
            target = GetTarget();
            startPos = new(_transform.position.x, _transform.position.y);
            startPos = startPos
                + (target - startPos).normalized
                * 1.3f;
            Shoot(startPos, target);
        }

        public void StopAutomatic()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            StopAutomatic();
            Clear();
        }

        protected override void OnEndReload()
        {
            base.OnEndReload();
            StartAutomatic();
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