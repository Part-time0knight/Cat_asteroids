using System;
using UnityEngine;
using Zenject;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Game.Logic.Effects.Explosion
{
    public class Explosion : MonoBehaviour
    {
        public event Action<Explosion> OnDespawn;

        private event Action<Explosion> OnAnimationEnd;

        private Animator _animator;
        private float _animatorPlaySpeed = 1f;

        private ParticleSystem _particleSystem;

        public void AnimationCallback()
        {
            OnAnimationEnd?.Invoke(this);
            _particleSystem.Stop();
        }

        public void Pause()
        {
            _animator.speed = 0;
            _particleSystem.Pause();
        }
        
        public void Continue()
        {
            _animator.speed = _animatorPlaySpeed;
            _particleSystem.Play();
        }

        [Inject]
        private void Construct(Animator animator, ParticleSystem particleSystem)
        {
            _animator = animator;
            _particleSystem = particleSystem;
        }

        private void Initialize(Vector2 spawnPoint, float sizeMultiplier)
        {
            transform.position = spawnPoint;
            transform.localScale = Vector3.one * sizeMultiplier;
            _animator.Rebind();
            _animator.Update(0f);

            _animator.Play(_animator.runtimeAnimatorController.animationClips[0].name);
            _animatorPlaySpeed = _animator.speed;
            _particleSystem.Play();
        }

        private void InvokeDespawn()
        {
            OnDespawn?.Invoke(this);
        }

        public class Pool : MonoMemoryPool<Vector2, float, Explosion>
        {
            protected override void OnSpawned(Explosion item)
            {
                base.OnSpawned(item);
                item.OnAnimationEnd += Despawn;
            }

            protected override void OnDespawned(Explosion item)
            {
                base.OnDespawned(item);
                item.OnAnimationEnd -= Despawn;
                item.InvokeDespawn();
            }

            protected override void Reinitialize(Vector2 spawnPoint, float sizeMultiplier, Explosion item)
            {
                base.Reinitialize(spawnPoint, sizeMultiplier, item);
                item.Initialize(spawnPoint, sizeMultiplier);
            }
        }
    }
}