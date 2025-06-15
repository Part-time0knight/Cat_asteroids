
using Game.Logic.Handlers;
using System;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public interface IPlayerShootHandler : IShootHandler
    {
        public bool Active { set; }
        public bool IsPause { set; }

        void Clear();

        void SetTarget(Func<Vector2> targetGetter);
        void SetPosition(Func<Vector2> positionGetter);
    }
}