using System;
using UnityEngine;

namespace Game.Logic.Projectiles
{
    public interface IProjectile 
    {
        event Action<IProjectile, GameObject> OnHit;
        public event Action<IProjectile> OnDead;

        void Initialize(Vector2 startPos, Vector2 targetPos);

        bool Pause { set; }
    }
}