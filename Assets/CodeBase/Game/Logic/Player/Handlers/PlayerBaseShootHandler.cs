using Cysharp.Threading.Tasks;
using Game.Logic.Handlers;
using Game.Logic.Projectiles;
using Game.Logic.StaticData;
using System;
using System.Threading;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class PlayerBaseShootHandler : ShootHandler, IDisposable, IPlayerShootHandler
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

        public bool IsPause
        {
            set
            {
                if (value)
                    Pause();
                else
                    Continue();
            }
        }

        public PlayerBaseShootHandler(ProjectileManager projectileManager,
            PlayerSettings settings,
            Transform weaponPoint,
            PlayerFacade playerFacade,
            IPlayerScoreWriter scoreWriter) : base(
                projectileManager,
                playerFacade,
                settings)
        {
            _weapon = weaponPoint;
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

        protected override void OnHit(UnitFacade unitHandler)
        {
            base.OnHit(unitHandler);
            if (unitHandler == null)
                return;
            if (unitHandler.Score > 0)
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