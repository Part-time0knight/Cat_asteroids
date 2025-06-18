using Game.Logic.Handlers;
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Logic.Player.Handlers
{
    public class ReactiveArmorShootHandler : ShootHandler
    {
        private readonly IPlayerScoreWriter _scoreWriter;

        private readonly ReactiveArmorSettings _reactiveSettings;
        private readonly Rigidbody2D _body;

        public ReactiveArmorShootHandler(Rigidbody2D body,
            ProjectileManager projectileManager,
            PlayerFacade unitFacade,
            IPlayerScoreWriter scoreWriter,
            ReactiveArmorSettings settings) : base(
                projectileManager,
                unitFacade,
                settings)
        {
            _body = body;
            _reactiveSettings = settings;

            _scoreWriter = scoreWriter;
        }

        public void Shoot()
        {
            Vector2 weaponPos, target;
            float angle, 
                step = 2f * Mathf.PI / _reactiveSettings.ProjectileCount;

            for (int i = 0; i < _reactiveSettings.ProjectileCount; i++)
            {
                angle = step * i;
                
                weaponPos = new Vector2(Mathf.Cos(angle) * _reactiveSettings.Radius,
                        Mathf.Sin(angle) * _reactiveSettings.Radius);
                
                target = weaponPos * _reactiveSettings.Radius;
                
                Shoot(_body.transform.TransformPoint(weaponPos), 
                    _body.transform.TransformPoint(target));
            }
        }

        protected override void OnHit(UnitFacade unitHandler)
        {
            base.OnHit(unitHandler);
            if (unitHandler == null)
                return;
            if (unitHandler.Score > 0)
                _scoreWriter.AddScore(unitHandler.Score, unitHandler.transform.position);
        }

        [Serializable]
        public class ReactiveArmorSettings : Settings
        {
            [field: SerializeField] public int ProjectileCount { get; private set; } = 8;

            [field: SerializeField] public float Radius { get; private set; } = 1.2f;
        }
    }
}