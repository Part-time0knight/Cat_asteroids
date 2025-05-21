using Game.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy.Ice
{
    public class IceBullet : Bullet
    {
        private ParticleSystem _particles;

        public override void Pause()
        {
            base.Pause();
            _particles.Pause();
        }

        public override void Continue()
        {
            base.Continue();
            _particles.Play();
        }

        [Inject]
        private void ConstructParticles(ParticleSystem particles)
        {
            _particles = particles;
        }

        public class IcePool : Pool
        {

        }
    }
}