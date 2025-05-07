using Cysharp.Threading.Tasks;
using Game.Logic.Handlers;
using Game.Logic.StaticData;
using Game.Logic.Weapon;
using System;
using System.Threading;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerShootHandler : ShootHandler, IDisposable
    {
        private readonly Transform _weapon;
        private readonly IPlayerScoreWriter _scoreWriter;

        private CancellationTokenSource _cts = null;

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

        protected override void OnHit(UnitHandler unitHandler)
        {
            base.OnHit(unitHandler);
            if (unitHandler == null)
                return;
            _scoreWriter.AddScore(unitHandler.Score, unitHandler.transform.position);
        }

        public void Dispose()
        {
            StopAutomatic();
        }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}