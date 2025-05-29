using System;
using UnityEngine;

namespace Game.Logic.Projectiles
{
    public interface IProjectile 
    {
        event Action<IProjectile, GameObject> InvokeHit;

        void Initialize(Vector2 startPos, Vector2 targetPos);

        void Continue();
        void Pause();
    }
}