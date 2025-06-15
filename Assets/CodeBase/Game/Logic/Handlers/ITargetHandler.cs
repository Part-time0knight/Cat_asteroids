using UnityEngine;

namespace Game.Logic.Handlers
{
    public interface ITargetHandler : IHandler
    {
        public Vector2 GetTarget();

        public Vector2 GetPosition();
    }
}