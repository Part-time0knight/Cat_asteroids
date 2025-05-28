using Game.Logic.Handlers;
using System;
using UnityEngine;

namespace Game.Logic.Projectiles
{
    public interface IShootHandler : IHandler
    {
        void Shoot(Vector2 weaponPos, Vector2 target, Action onReloadEnd = null);
    }
}