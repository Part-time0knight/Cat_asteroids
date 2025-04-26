using System;
using UnityEngine;

namespace Game.Logic.Handlers
{
    public abstract class DamageHandler
    {
        protected readonly Settings _stats;

        public DamageHandler(Settings stats) 
        {
            _stats = stats;
            _stats.CurrentHits = _stats.HitPoints;
        }


        public void TakeDamage(int damage)
        {
            _stats.CurrentHits -= damage;
            _stats.CurrentHits = Mathf.Max(Mathf.Min(_stats.CurrentHits, _stats.HitPoints), 0);
            _stats.InvokeHitPointsChange?.Invoke();
        }

        public class Settings
        {
            [field: SerializeField] public int HitPoints { get; protected set; }
            public int CurrentHits { get; set; }

            public Action InvokeHitPointsChange;

            public Settings()
            { }

            public Settings(int hitPoints, int currentHits)
            {
                HitPoints = hitPoints;
                CurrentHits = currentHits;
            }

            public Settings(Settings settings) : this(
                settings.HitPoints,
                settings.CurrentHits)
            { }
        }
    }
}