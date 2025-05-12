using Game.Logic.Enemy;
using System;
using UnityEngine;

public class IceMMoveHandler : EnemyMoveHandler
{
    public IceMMoveHandler(Rigidbody2D body, IceSettings stats) : base(body, stats)
    {
    }

    [Serializable]
    public class IceSettings : EnemySettings
    {
    }
}
