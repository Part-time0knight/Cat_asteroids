using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Effects.Explosion
{
    public class ExplosionSpawner
    {
        private readonly Explosion.Pool _pool;
        private readonly List<Explosion> _explosions;

        public ExplosionSpawner(Explosion.Pool pool) 
        {
            _pool = pool;
            _explosions = new();
        }

        public void Spawn(Vector2 spawnPoint)
        {
            var item = _pool.Spawn(spawnPoint);
            _explosions.Add(item);
            item.OnDespawn += InvokeDespawn;
        }

        private void InvokeDespawn(Explosion explosion)
        {
            _explosions.Remove(explosion);
        }
    }
}