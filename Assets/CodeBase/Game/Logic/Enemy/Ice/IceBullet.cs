using Game.Logic.Projectiles;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy.Ice
{
    public class IceBullet : Bullet
    {
        private ParticleSystem _particles;

        protected override void SetPause()
        {
            if (_particles == null) return;
            base.SetPause();
            _particles.Pause();

        }

        protected override void Continue()
        {
            base.Continue();
            _particles.Play();
        }

        protected override void InvokeHit(GameObject objectHit)
        {
            if (objectHit.tag == "Border")
                Debug.Log("Ice strike border!");
            base.InvokeHit(objectHit);
        }

        [Inject]
        private void ConstructParticles(ParticleSystem particles)
        {
            _particles = particles;
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