using Game.Logic.Misc;
using System;
using UnityEngine;

namespace Game.Logic.Enemy.Ice
{
    public class IceBulletMoveHandler : BulletMoveHandler
    {
        public IceBulletMoveHandler(Rigidbody2D body, IceBulletSettings stats) : base(body, stats)
        {
        }

        [Serializable]
        public class IceBulletSettings : BulletSettings
        {

        }
    }
}