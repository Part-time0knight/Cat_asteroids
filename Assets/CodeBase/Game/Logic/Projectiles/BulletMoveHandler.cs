using Game.Logic.Handlers;
using System;

using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMoveHandler : MoveHandler
    {
        public BulletMoveHandler(Rigidbody2D body, BulletSettngs stats) : base(body, stats)
        { }

        [Serializable]
        public class BulletSettngs : Settings
        { }
    }
}