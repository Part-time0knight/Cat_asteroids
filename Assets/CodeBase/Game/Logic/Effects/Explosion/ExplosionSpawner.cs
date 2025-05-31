using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Effects.Explosion
{
    public class ExplosionSpawner
    {
        [Inject(Id = "Explosion")] private readonly Explosion.Pool _pool;
        [Inject(Id = "IceExplosion")] private readonly Explosion.Pool _icePool;
        private readonly List<Explosion> _explosions = new();


        public void Spawn(Vector2 spawnPoint, float size)
        {
            var item = _pool.Spawn(spawnPoint, size);
            _explosions.Add(item);
            item.OnDespawn += InvokeDespawn;
        }

        public void SpawnIce(Vector2 spawnPoint, float size)
        {
            var item = _icePool.Spawn(spawnPoint, size);
            _explosions.Add(item);
            item.OnDespawn += InvokeDespawn;
        }

        public void Pause()
        {
            foreach (var item in _explosions)
                item.Pause();
        }

        public void Continue()
        {
            foreach (var item in _explosions)
                item.Continue();
        }

        private void InvokeDespawn(Explosion explosion)
        {
            _explosions.Remove(explosion);
        }
    }
}