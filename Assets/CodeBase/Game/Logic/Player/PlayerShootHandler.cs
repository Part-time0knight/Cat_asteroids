using Cysharp.Threading.Tasks;
using Game.Logic.Handlers;
using Game.Logic.StaticData;
using Game.Logic.Weapon;
using System;
using System.Threading;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerShootHandler : ShootHandler, IDisposable, IPlayerShootHandler
    {
        private readonly Transform _weapon;
        private readonly IPlayerScoreWriter _scoreWriter;

        private CancellationTokenSource _cts = null;

        public bool Active 
        {
            set 
            {
                if (value)
                    StartAutomatic();
                else
                    StopAutomatic();
            } 
        }

        public bool Pause 
        {
            set
            {
                if (value)
                    SetPause();
                else
                    Continue();
            }
        }

        public PlayerShootHandler(Bullet.Pool bulletPool, 
            PlayerSettings settings,
            Transform weaponPoint,
            IPlayerScoreWriter scoreWriter) : base(bulletPool, settings)
        {
            _weapon = weaponPoint;
            _settings.Owner = Tags.Player;
            _scoreWriter = scoreWriter;
        }

        public void StartAutomatic()
        {
            if (_cts != null)
                return;
            _cts = new();
            Repeater().Forget();
        }

        public void StopAutomatic()
        { 
            if (_cts == null)
                return;
            _cts.Cancel();
            _cts = null;
        }

        public void Dispose()
        {
            StopAutomatic();
        }

        protected override void OnHit(UnitHandler unitHandler)
        {
            base.OnHit(unitHandler);
            if (unitHandler == null)
                return;
            _scoreWriter.AddScore(unitHandler.Score, unitHandler.transform.position);
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active, cancellationToken: _cts.Token);
                Vector2 target = _weapon.TransformPoint(
                    new(_weapon.localPosition.x, _weapon.localPosition.y + 1f));
                if (!_cts.IsCancellationRequested)
                    Shoot(_weapon.position, target);
            } while (!_cts.IsCancellationRequested);
        }


        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}