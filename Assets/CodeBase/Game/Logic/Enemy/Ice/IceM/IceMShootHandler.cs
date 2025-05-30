using Game.Logic.Handlers;
using Game.Logic.Player;
using System;
using UnityEngine;

namespace Game.Logic.Enemy.Ice.IceM
{
    public class IceMShootHandler : IceShootHandler
    {
        public IceMShootHandler(IPlayerPositionReader playerPositionReader,
            IEnemyPositionReader enemyPositionReader,
            Transform transform,
            ProjectileManager projectileManager,
            EnemyFacade iceFacade,
            IceMSettings settings) 
            : base(playerPositionReader,
                  enemyPositionReader,
                  iceFacade,
                  transform, projectileManager,
                  settings)
        {
        }

        [Serializable]
        public class IceMSettings : IceSettings { }
    }
}