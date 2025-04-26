using Cysharp.Threading.Tasks;
using Game.Logic.Handlers;
using Game.Logic.Misc;
using Game.Logic.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.EventSystems.EventTrigger;

namespace Game.Logic.Enemy
{
    public class EnemySpawner : IPauseble, IInitializable, IDisposable, IFixedTickable
    {
        public Vector2 NearestEnemy;

        private readonly IPauseHandler _pauseHandler;
        private readonly EnemyHandler.Pool _pool;
        private readonly Timer _timer;
        private readonly Settings _settings;
        private readonly PlayerMoveHandler.PlayerSettings _playerSettings;

        private readonly List<EnemyHandler> _enemies;

        private bool _breakTimer;
        private Vector2 _position;

        public EnemySpawner(IPauseHandler pauseHandler,
            EnemyHandler.Pool pool, Settings settings,
            PlayerMoveHandler.PlayerSettings playerSettings)
        {
            _pauseHandler = pauseHandler;
            _pool = pool;
            _timer = new();
            _settings = settings;
            _playerSettings = playerSettings;
            _enemies = new();
        }

        public void Initialize()
        {
            _pauseHandler.SubscribeElement(this);
            BeginSpawn();
        }

        public void Dispose()
        {
            _pauseHandler.UnsubscribeElement(this);
        }

        public void OnPause(bool active)
        {
            if (_timer.Active)
                if (active)
                    _timer.Pause();
                else
                    _timer.Play();
        }

        public void BeginSpawn()
        {
            _breakTimer = false;
            Repeater();
        }

        public void StopSpawn()
        {
            _breakTimer = true;
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active);
                if (!_breakTimer)
                {
                    _timer.Initialize(_settings.Delay).Play();
                    CalculatePosition();
                    var enemy = _pool.Spawn(_position);
                    enemy.InvokeDeath += OnDeath;
                    _enemies.Add(enemy);
                }
            } while (!_breakTimer);
        }

        private void OnDeath(EnemyHandler enemyHandler)
        {
            _enemies.Remove(enemyHandler);
            _pool.Despawn(enemyHandler);
            enemyHandler.InvokeDeath -= OnDeath;
        }

        private void CalculatePosition()
        {
            _position = Vector2.zero;
            int randPoint = UnityEngine.Random.Range(0, 2);
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                _position.x = randPoint == 0 ?
                    _settings.HorizontalBorders.y : _settings.HorizontalBorders.x;
                
                _position.y = UnityEngine.Random.
                    Range(_settings.VerticalBorders.x, _settings.VerticalBorders.y);
            }
            else
            {
                _position.y = randPoint == 0 ?
                    _settings.VerticalBorders.y : _settings.VerticalBorders.x;

                _position.x = UnityEngine.Random.
                    Range(_settings.HorizontalBorders.x, _settings.HorizontalBorders.y);
            }

        }

        public void FixedTick()
        {
            if (_enemies.Count == 0)
                return;
            NearestEnemy = _enemies[0].GetPosition();
            foreach (var enemy in _enemies)
            {
                if (Vector2.Distance(_playerSettings.CurrentPosition, enemy.GetPosition()) <
                    Vector2.Distance(NearestEnemy, _playerSettings.CurrentPosition))
                    NearestEnemy = enemy.GetPosition();
            }
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Vector2 HorizontalBorders { get; private set; }
            [field: SerializeField] public Vector2 VerticalBorders { get; private set; }
            [field: SerializeField] public float Delay { get; private set; }
        }
    }
}