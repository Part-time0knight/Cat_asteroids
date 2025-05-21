using Game.Logic.Handlers;
using System;

using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Logic.Misc
{
    public class BulletMoveHandler : MoveHandler
    {
        public BulletMoveHandler(Rigidbody2D body, BulletSettings stats) : base(body, stats)
        { }

        public virtual void Rotate(Quaternion quaternion)
        {
            
        }

        [Serializable]
        public class BulletSettings : Settings
        { }
    }
}