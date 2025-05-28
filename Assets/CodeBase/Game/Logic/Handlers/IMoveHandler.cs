
using System;
using UnityEngine;

namespace Game.Logic.Handlers
{
    public interface IMoveHandler : IHandler
    {
        event Action<GameObject> OnTrigger;
        event Action<GameObject> OnCollision;

        void Move(Vector2 speedMultiplier);
        void Stop();
        void Pause();
        void Continue();
    }
}