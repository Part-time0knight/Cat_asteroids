using DG.Tweening;
using Game.Logic.Misc;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

namespace Game.Logic.Projectiles
{
    public class Laser : MonoBehaviour, IProjectile
    {
        public event Action<IProjectile, GameObject> OnHit;
        public event Action<IProjectile> OnDead;

        protected Vector2 _direction = Vector2.zero;
        protected Vector2 _endPoint = Vector2.zero;

        private LineRenderer _laserLine;
        private Settings _settings;
        private Sequence _sequence;

        private RaycastHit2D _hit;

        private readonly Timer _timer = new();

        protected ProjectileDamageHandler _damageHandler;

        bool IProjectile.Pause 
        {
            set
            {
                if (value)
                    SetPause();
                else
                    Continue();
            }
        }

        protected virtual void SetPause()
        {
            _sequence.Pause();
        }

        protected virtual void Continue()
        {
            _sequence.Play();
        }



        public void Initialize(Vector2 startPos, Vector2 targetPos)
        {
            transform.position = startPos;
            _direction = (targetPos - startPos).normalized;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            Fire();
        }

        protected virtual void Awake()
        {
            _damageHandler.OnDeath += InvokeDeath;
        }

        [Inject]
        private void Construct(LineRenderer laserLine,
            ProjectileDamageHandler projectileDamageHandler,
            Settings settings)
        {
            _laserLine = laserLine;
            _settings = settings;
            _damageHandler = projectileDamageHandler;
            _laserLine.enabled = false;
            _laserLine.startWidth = _settings.Laser.Width;
            _laserLine.endWidth = _settings.Laser.Width;
            _laserLine.material = _laserLine.materials[0];
        }

        private void Fire()
        {
            foreach (var mask in _settings.Laser.TargetLayers)
            {
                _hit = Physics2D.Raycast(transform.position, _direction, 100, mask.value);
                if (_hit.transform != null)
                    break;
            }
            
            _endPoint = _hit.point;
            _laserLine.SetPositions(new Vector3[] { transform.position, _endPoint });
            _laserLine.enabled = true;

            _timer.Initialize(0.01f, Hit).Play();


            LaserAnimation();
            
        }

        private void LaserAnimation()
        {
            AnimationClear();
            _sequence = DOTween.Sequence();
            _sequence.Join(_laserLine.DOColor(
                    new(_settings.Laser.StartColor, _settings.Laser.StartColor),
                    new(_settings.Laser.FinishColor, _settings.Laser.FinishColor), 
                    _settings.Laser.Duration)
                ).Play()
                .OnComplete(OnAnimationEnd);
        }

        private void Hit()
        {
            OnHit?.Invoke(this, _hit.collider.gameObject);
        }

        private void OnAnimationEnd()
        {
            _laserLine.enabled = false;
        }

        private void AnimationClear()
        {
            if (_sequence == null) return;
            _sequence.Kill();
            _sequence = null;
        }

        protected virtual void OnDestroy()
        {
            AnimationClear();
            _damageHandler.OnDeath -= InvokeDeath;
        }

        private void InvokeDeath()
        {
            OnDead?.Invoke(this);
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, Laser>, IProjectilePool
        {
            public void DespawnProjectile(IProjectile projectile)
            {
                Laser laser = projectile as Laser;
                if (laser == null) return;

                if (laser._sequence.IsPlaying())
                    laser._sequence.OnComplete(() =>
                    {
                        Despawn(projectile as Laser);
                    });
                else
                    Despawn(projectile as Laser);


            }

            public IProjectile SpawnProjectile(Vector2 startPos, Vector2 target)
                => Spawn(startPos, target);

            /// <param name="startPos">World space position</param>
            /// <param name="targetPos">World space position</param>
            protected override void Reinitialize(Vector2 startPos, Vector2 targetPos, Laser item)
            {
                base.Reinitialize(startPos, targetPos, item);
                item.Initialize(startPos, targetPos);
            }
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public LaserSettings Laser { get; private set; }

            [Serializable]
            public class LaserSettings
            {
                [field: SerializeField] public float Width { get; private set; } = 0.1f;
                [field: SerializeField] public float Duration { get; private set; } = 0.3f;
                [field: SerializeField] public Color StartColor { get; private set; } = Color.green;
                [field: SerializeField] public Color FinishColor { get; private set; } = Color.green;
                [field: SerializeField] public List<LayerMask> TargetLayers { get; private set; }
            }
        }
    }
}