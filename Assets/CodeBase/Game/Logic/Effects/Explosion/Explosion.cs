using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Effects.Explosion
{
    public class Explosion : MonoBehaviour
    {
        private event Action<Explosion> OnAnimationEnd;

        private Animator _animator;
        private ParticleSystem _particleSystem;

        public void AnimationCallback()
        {
            OnAnimationEnd?.Invoke(this);
            _particleSystem.Stop();
        }

        [Inject]
        private void Construct(Animator animator, ParticleSystem particleSystem)
        {
            _animator = animator;
            _particleSystem = particleSystem;
        }

        private void Initialize(Vector2 spawnPoint)
        {
            transform.position = spawnPoint;
            _animator.Rebind();
            _animator.Update(0f);

            _animator.Play(_animator.runtimeAnimatorController.animationClips[0].name);
            _particleSystem.Play();
        }

        public class Pool : MonoMemoryPool<Vector2, Explosion>
        {
            protected Transform _buffer;

            [Inject]
            private void Construct(ExplosionBuffer buffer)
            {
                _buffer = buffer.transform;
            }

            protected override void OnCreated(Explosion item)
            {
                item.transform.SetParent(_buffer);
                base.OnCreated(item);
            }

            protected override void OnSpawned(Explosion item)
            {
                base.OnSpawned(item);
                item.OnAnimationEnd += Despawn;
            }

            protected override void OnDespawned(Explosion item)
            {
                base.OnDespawned(item);
                item.OnAnimationEnd -= Despawn;
            }

            protected override void Reinitialize(Vector2 spawnPoint, Explosion item)
            {
                base.Reinitialize(spawnPoint, item);
                item.Initialize(spawnPoint);
            }
        }
    }
}