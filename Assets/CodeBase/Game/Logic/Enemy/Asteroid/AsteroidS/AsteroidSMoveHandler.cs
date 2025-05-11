using Game.Logic.Enemy;
using System;
using UnityEngine;

public class AsteroidSMoveHandler : EnemyMoveHandler
{
    public AsteroidSMoveHandler(Rigidbody2D body, AsteroidSettings stats) : base(body, stats)
    {
    }

    [Serializable]
    public class AsteroidSettings : EnemySettings
    {
    }
}
