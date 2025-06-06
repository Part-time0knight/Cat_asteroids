using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player.Handlers
{
    public class PlayerWeaponHandler : WeaponHandler
    {
        private readonly IPlayerScoreWriter _scoreWriter;

        public PlayerWeaponHandler(IPlayerScoreWriter scoreWriter,
            PlayerSettings settings) : base(settings)
        {
            _scoreWriter = scoreWriter;
        }

        protected override void MakeDamage()
        {
            base.MakeDamage();
            if (_target == null)
                return;
            if (_target.Score > 0)
                _scoreWriter.AddScore(_target.Score, _target.transform.position);
        }

        [Serializable]
        public class PlayerSettings : Settings
        {

        }
    }
}