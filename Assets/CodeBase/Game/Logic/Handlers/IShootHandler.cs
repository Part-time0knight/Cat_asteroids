using System;
using UnityEngine;

namespace Game.Logic.Handlers
{
    public interface IShootHandler : IHandler
    {
        void Shoot(Vector2 weaponPos, Vector2 target, Action onReloadEnd = null);
    }
}