using Cysharp.Threading.Tasks;
using Game.Logic.Player;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;
using Timer = Game.Logic.Misc.Timer;

namespace Game.Logic.Enemy.Spawner
{
    public class EnemySpawner : ISpawner, IDisposable
    {

        private readonly IPlayerPositionReader _playerDataReader;
        private readonly Timer _timer;

        private readonly EnemyHandler.Pool _pool;
        private readonly ISpawner.Settings _settings;

        private readonly List<EnemyHandler> _enemies;

        private Vector2 _position;
        private Vector2 _direction;

        private CancellationTokenSource _cts = null;

        public bool Active => _cts != null;

        public EnemySpawner(EnemyHandler.Pool pool,
            ISpawner.Settings settings,
            IPlayerPositionReader playerDataReader)
        {
            _timer = new();
            _enemies = new();
            _pool = pool;
            _settings = settings;
            _playerDataReader = playerDataReader;
        }

        public void Start()
        {
            if (_cts != null)
                return;
            _cts = new();
            _timer.Initialize(_settings.Delay).Play();
            Repeater().Forget();
        }

        public void Stop()
        {
            if (_cts == null)
                return;
            _cts.Cancel();
            _cts = null;
        }

        public void Clear()
        {
            foreach (var enemy in _enemies)
                enemy.Clear();

            while (_enemies.Count > 0)
                OnDeath(_enemies[0]);
        }

        public void Pause()
        {
            if (_timer.Active)
                _timer.Pause();
            foreach (var enemy in _enemies)
                enemy.Pause = true;
        }

        public void Continue()
        {
            if (_timer.Active)
                _timer.Play();
            foreach (var enemy in _enemies)
                enemy.Pause = false;
        }

        public void Dispose()
        {
            Stop();
            Clear();
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active, 
                    cancellationToken: _cts.Token);
                _timer.Initialize(_settings.Delay).Play();
                Spawn();
            } while (!_cts.IsCancellationRequested);
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

        public List<Vector2> GetPositions()
        {
            List<Vector2> positions = new();
            foreach (var enemy in _enemies)
                positions.Add(enemy.transform.position);
            return positions;
        }
    }
}