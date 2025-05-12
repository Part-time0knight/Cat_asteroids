
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Enemy
{
    public interface IEnemyPositionReader
    {

        /// <summary>
        /// Get the position of the nearest enemy
        /// </summary>
        /// <param name="point">The point to which to calculate the nearest enemies</param>
        /// <returns></returns>
        Vector2 GetNearest(Vector2 point);

    }
}