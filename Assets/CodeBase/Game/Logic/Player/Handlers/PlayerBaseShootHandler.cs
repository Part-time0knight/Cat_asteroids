using Cysharp.Threading.Tasks;
using Game.Logic.Handlers;
using System;
using System.Threading;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class PlayerBaseShootHandler : ShootHandler, IDisposable, IPlayerShootHandler
    {

        private readonly IPlayerScoreWriter _scoreWriter;

        private Func<Vector2> _targetGetter;
        private Func<Vector2> _positionGetter;

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
            PlayerFacade playerFacade,
            IPlayerScoreWriter scoreWriter) : base(
                projectileManager,
                playerFacade,
                settings)
        {
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
            Clear();
        }


        public void SetTarget(Func<Vector2> targetGetter)
        {
            _targetGetter = targetGetter;
        }

        public void SetPosition(Func<Vector2> positionGetter)
        {
            _positionGetter = positionGetter;
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
            Vector2 target;
            Vector2 position;
            do
            {
                await UniTask.WaitWhile(() => _timer.Active, cancellationToken: _cts.Token);
                target = _targetGetter.Invoke();
                position = _positionGetter.Invoke();
                if (!_cts.IsCancellationRequested)
                    Shoot(position, target);
            } while (!_cts.IsCancellationRequested);
        }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}