using Cysharp.Threading.Tasks;
using Game.Logic.StaticData;
using Game.Logic.Weapon;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerShootHandler : ShootHandler
    {
        private readonly Transform _weapon;

        private bool _breakAutomatic = false;

        public PlayerShootHandler(Bullet.Pool bulletPool, 
            PlayerSettings settings,
            Transform weaponPoint) : base(bulletPool, settings)
        {
            _weapon = weaponPoint;
            _settings.Owner = Tags.Player;
        }

        public void StartAutomatic()
        {
            _breakAutomatic = false;
            Repeater();
        }

        public void StopAutomatic()
        { 
            _breakAutomatic = true;
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active);
                Vector2 target = _weapon.TransformPoint(
                    new(_weapon.localPosition.x, _weapon.localPosition.y + 1f));
                if (!_breakAutomatic)
                    Shoot(_weapon.position, target);
            } while (!_breakAutomatic);
        }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}