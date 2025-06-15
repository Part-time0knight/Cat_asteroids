using Game.Logic.Enemy;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class PlayerAimHandler : IPlayerTargetHandler
    {
        private readonly Transform _weapon;
        private readonly IEnemyPositionReader _reader;
        private readonly Rigidbody2D _body;

        public PlayerAimHandler(IEnemyPositionReader enemyPositionReader,
            Transform weaponPoint,
            Rigidbody2D body)
        {
            _body = body;
            _weapon = weaponPoint;
            _reader = enemyPositionReader;
        }

        public Vector2 GetPosition()
        {
            var point = _reader.GetNearest(_weapon.position);
            if (point == Vector2.zero)
                return _weapon.position;
            point = point.normalized * 1.2f + (Vector2)_body.transform.position;
            return point;
        }

        public Vector2 GetTarget()
        {
            var point = _reader.GetNearest(_weapon.position);
            if (point == Vector2.zero)
                point = _weapon.TransformPoint(
                    new(_weapon.localPosition.x, _weapon.localPosition.y + 1f));
            return point;
        }
    }
}