using UnityEngine;

namespace Game.Logic.Effects.Explosion
{
    public class ExplosionSpawner
    {
        private readonly Explosion.Pool _pool;

        public ExplosionSpawner(Explosion.Pool pool) 
        {
            _pool = pool;
        }

        public void Spawn(Vector2 spawnPoint)
        {
            _pool.Spawn(spawnPoint);
        }
    }
}