using Game.Logic.Handlers;
using System;

namespace Game.Logic.Enemy
{
    public class EnemyWeaponHandler : WeaponHandler
    {
        public EnemyWeaponHandler(EnemySettings settings) : base(settings)
        {

        }

        [Serializable]
        public class EnemySettings : Settings
        {
        }
    }
}