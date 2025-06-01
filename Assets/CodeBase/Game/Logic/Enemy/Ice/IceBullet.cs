using Game.Logic.Effects.Explosion;
using Game.Logic.Misc;
using Game.Logic.Projectiles;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy.Ice
{
    public class IceBullet : Bullet
    {
        private ParticleSystem _hasteParticles;
        private ExplosionSpawner _explosionSpawner;

        protected override void SetPause()
        {
           
            base.SetPause();
            _hasteParticles.Pause();

        }

        protected override void Continue()
        {
            base.Continue();
            _hasteParticles.Play();
        }


        protected override void InvokeDeath()
        {
            _explosionSpawner.SpawnIce(transform.position, 0.75f);
            base.InvokeDeath();
        }


        [Inject]
        private void ConstructParticles(ParticleSystem particles, ExplosionSpawner explosionSpawner)
        {
            _hasteParticles = particles;
            _explosionSpawner = explosionSpawner;
        }

        public class IcePool : Pool
        {
            public override void DespawnProjectile(IProjectile projectile)
            {
                base.DespawnProjectile(projectile);
            }
        }
    }
}