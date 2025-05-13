using Game.Logic.Enemy;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy.Ice.IceM
{
    public class IceMMoveHandler : IceMoveHandler
    {

        public IceMMoveHandler(Rigidbody2D body, IceMSettings stats) : base(body, stats)
        {
        }


        [Serializable]
        public class IceMSettings : IceSettings { }
    }
}