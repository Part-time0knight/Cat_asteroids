using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player
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
            _scoreWriter.AddScore(_target.Score, _target.transform.position);
        }

        [Serializable]
        public class PlayerSettings : Settings
        {

        }
    }
}