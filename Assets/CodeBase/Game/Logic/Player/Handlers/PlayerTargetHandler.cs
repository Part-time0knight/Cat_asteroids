using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class PlayerTargetHandler : IPlayerTargetHandler
    {
        public Transform _weapon;

        public PlayerTargetHandler(Transform weaponPoint) 
        {
            _weapon = weaponPoint;
        }

        public Vector2 GetPosition()
            => _weapon.position;

        public Vector2 GetTarget()
            => _weapon.TransformPoint(
                    new(_weapon.localPosition.x, _weapon.localPosition.y + 1f));
    }
}