using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace Game.Logic.Enemy.Spawner
{
    public class EnemySpawnerService
    {
        private readonly Dictionary<string, ISpawner> _spawners;
        private readonly SpawnerFactory _factory;
        private readonly DiContainer _container;

        public EnemySpawnerService(DiContainer container, SpawnerFactory factory)
        {
            _spawners = new();
            _factory = factory;
            _container = container;
        }

        public void Start(string id)
        {
            if (!_spawners.ContainsKey(id))
                ResolveSpawner(id);
            if (_spawners[id].Active)
                _spawners[id].Stop();
            _spawners[id].Start();
        }

        public void Stop(string id)
        {
            if (!_spawners.ContainsKey(id))
            {
                Debug.LogError("Not found spawner to stop with id: " + id);
                return;
            }
            _spawners[id].Stop();
        }

        public void Clear(string id)
        {
            if (!_spawners.ContainsKey(id))
            {
                Debug.LogError("Not found spawner to clear with id: " + id);
                return;
            }
            _spawners[id].Clear();
        }

        public void ClearAll()
        {
            foreach (var spawner in _spawners.Values)
                spawner.Clear();
        }

        public void PauseAll()
        {
            foreach (var spawner in _spawners.Values)
                spawner.Pause();
        }

        public void ContinueAll()
        {
            foreach (var spawner in _spawners.Values)
                spawner.Continue();
        }

        private void ResolveSpawner(string id)
        {
            var settings = _container.TryResolveId<ISpawner.Settings>(id);
            if (settings == null)
            {
                Debug.LogError("Not found settings for spawner with id: " + id);
                return;
            }

            var pool = _container.TryResolveId<EnemyHandler.Pool>(id);
            if (pool == null)
            {
                Debug.LogError("Not found pool for spawner with id: " + id);
                return;
            }

            var spawner = _factory.Create(pool, settings);
            _spawners.Add(id, spawner);
        }
    }
}