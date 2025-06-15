using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Game.Logic.Handlers
{
    public abstract class DamageHandler : IDamageHandler
    {
        public event Action<int> OnTakeDamage;
        public event Action OnDeath;

        protected int _hits;

        protected readonly Settings _stats;

        public DamageHandler(Settings stats) 
        {
            _stats = stats;
            _hits = _stats.HitPoints;
        }


        public virtual void TakeDamage(int damage)
        {
            _hits -= damage;
            _hits = Mathf.Max(Mathf.Min(_hits, _stats.HitPoints), 0);
            
            if (_hits <= 0)
                OnDeath?.Invoke();
            else
                OnTakeDamage?.Invoke(_hits);
        }

        public virtual void Reset()
        {
            _hits = _stats.HitPoints;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int HitPoints { get; protected set; }
        }
    }
}