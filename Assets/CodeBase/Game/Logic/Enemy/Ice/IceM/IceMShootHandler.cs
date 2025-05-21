using Game.Logic.Player;
using Game.Logic.Weapon;
using System;
using UnityEngine;

namespace Game.Logic.Enemy.Ice.IceM
{
    public class IceMShootHandler : IceShootHandler
    {
        public IceMShootHandler(IPlayerPositionReader playerPositionReader,
            IEnemyPositionReader enemyPositionReader,
            Transform transform,
            IceBullet.IcePool bulletPool,
            IceMSettings settings) 
            : base(playerPositionReader,
                  enemyPositionReader,
                  transform, bulletPool,
                  settings)
        {
        }

        [Serializable]
        public class IceMSettings : IceSettings { }
    }
}