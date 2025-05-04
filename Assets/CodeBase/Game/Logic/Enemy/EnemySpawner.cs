using Cysharp.Threading.Tasks;
using Game.Logic.Handlers;
using Game.Logic.Misc;
using Game.Logic.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Logic.Enemy
{
    public class EnemySpawner : IDisposable
    {

        private readonly IPlayerPositionReader _playerDataReader;
        private readonly EnemyHandler.Pool _pool;
        private readonly Timer _timer;
        private readonly Settings _settings;

        private readonly List<EnemyHandler> _enemies;

        private bool _breakTimer;
        private Vector2 _position;
        private Vector2 _direction;

        public EnemySpawner(EnemyHandler.Pool pool,
            Settings settings,
            IPlayerPositionReader playerDataReader)
        {
            _pool = pool;
            _timer = new();
            _settings = settings;
            _enemies = new();
            _playerDataReader = playerDataReader;
        }

        public void BeginSpawn()
        {
            _breakTimer = false;
            Repeater();
        }

        public void StopSpawn()
        {
            _breakTimer = true;
            while (_enemies.Count > 0)
                OnDeath(_enemies[0]);
        }

        public void Dispose()
        {
            StopSpawn();
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active);
                if (_breakTimer)
                    return;
                _timer.Initialize(_settings.Delay).Play();
                Spawn();
            } while (!_breakTimer);
        }

        private void Spawn()
        {
            CalculatePosition();
            var enemy = _pool.Spawn(_position, _direction);
            enemy.OnDeath += OnDeath;
            enemy.OnDeactivate += OnDeactivate;
            _enemies.Add(enemy);
        }

        private void OnDeath(EnemyHandler enemyHandler)
        {
            _enemies.Remove(enemyHandler);
            _pool.Despawn(enemyHandler);
            enemyHandler.OnDeath -= OnDeath;
            enemyHandler.OnDeactivate -= OnDeactivate;
        }

        private void OnDeactivate(EnemyHandler enemyHandler)
        {
            _enemies.Remove(enemyHandler);
            _pool.Despawn(enemyHandler);
            enemyHandler.OnDeath -= OnDeath;
            enemyHandler.OnDeactivate -= OnDeactivate;
        }

        private void CalculatePosition()
        {
            _position = Vector2.zero;
            Vector2 position, target;


            do
            {
                bool isHorizontal = Random.Range(0, 2) == 0 ? true : false;
                bool isTop = Random.Range(0, 2) == 0 ? true : false;
                float posX, posY;
                float targetX, targetY;
                if (isHorizontal)
                {
                    posX = Random.Range(_settings.HorizontalBorders.x, _settings.HorizontalBorders.y);
                    targetX = Random.Range(_settings.HorizontalBorders.x, _settings.HorizontalBorders.y);
                    if (isTop)
                    {
                        posY = _settings.VerticalBorders.y;
                        targetY = _settings.VerticalBorders.x;
                    }
                    else
                    {
                        posY = _settings.VerticalBorders.x;
                        targetY = _settings.VerticalBorders.y;
                    }
                }
                else
                {
                    posY = Random.Range(_settings.VerticalBorders.x, _settings.VerticalBorders.y);
                    targetY = Random.Range(_settings.VerticalBorders.x, _settings.VerticalBorders.y);
                    if (isTop)
                    {
                        posX = _settings.HorizontalBorders.x;
                        targetX = _settings.HorizontalBorders.y;
                    }
                    else
                    {
                        posX = _settings.HorizontalBorders.y;
                        targetX = _settings.HorizontalBorders.x;
                    }
                }
                position = new(posX, posY);
                target = new(targetX, targetY);
            } while (
                Vector2.Distance(position, _playerDataReader.Position) <
                _settings.MinimalRangeToPlayer
                );
            _position = position;
            _direction = (target - position).normalized;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Vector2 HorizontalBorders { get; private set; }
            [field: SerializeField] public Vector2 VerticalBorders { get; private set; }
            [field: SerializeField] public float MinimalRangeToPlayer { get; private set; }
            [field: SerializeField] public float Delay { get; private set; }
        }
    }
}