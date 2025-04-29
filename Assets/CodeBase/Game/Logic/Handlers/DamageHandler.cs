using System;
using UnityEngine;

namespace Game.Logic.Handlers
{
    public abstract class DamageHandler
    {
        public Action<int> OnTakeDamage;
        public Action OnDeath;

        protected int _hits;

        protected readonly Settings _stats;

        public DamageHandler(Settings stats) 
        {
            _stats = stats;
            _hits = _stats.HitPoints;
        }


        public void TakeDamage(int damage)
        {
            _hits -= damage;
            _hits = Mathf.Max(Mathf.Min(_hits, _stats.HitPoints), 0);
            OnTakeDamage?.Invoke(_hits);
            if (_hits <= 0)
                OnDeath?.Invoke();
            Debug.Log("take damage! " + GetType().ToString());
        }

        public class Settings
        {
            [field: SerializeField] public int HitPoints { get; protected set; }

            public Settings()
            { }

            public Settings(int hitPoints)
            {
                HitPoints = hitPoints;
            }

            public Settings(Settings settings) : this(
                settings.HitPoints)
            { }
        }
    }
}