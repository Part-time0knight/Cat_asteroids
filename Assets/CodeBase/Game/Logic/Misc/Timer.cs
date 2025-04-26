using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class Timer
    {
        private bool _active;

        private Action _invokeComplete;
        private Action<float> _invokeTick;

        private CancellationTokenSource _cts;

        private float _time;

        private float _currentTime;

        private float _step;

        public bool Active => _active;

        public Timer()
        {
            _active = false;
        }

        public Timer Initialize(float time, Action callback = null)
        {
            return Initialize(time, 0.1f, null, callback);
        }

        public Timer Initialize(float time, float step, Action callback)
        {
            return Initialize(time, step, null, callback);
        }

        public Timer Initialize(float time, Action<float> callTick, Action callback)
        {
            return Initialize(time, 0.1f, callTick, callback);
        }

        public Timer Initialize(float time, float step, Action<float> callTick, Action callback)
        {
            _time = time;
            _currentTime = _time;
            _invokeComplete = callback;
            _invokeTick = callTick;
            _step = step;

            return this;
        }

        public void Play()
        {
            if (_currentTime == 0)
                return;
            _active = true;
            _cts = new();
            ExecuteAsync();
        }

        public void Pause()
            => _cts.Cancel();

        public void Stop()
        {
            _currentTime = 0;
            _invokeTick?.Invoke(_currentTime);
            _cts.Cancel();
        }

        private async UniTask ExecuteAsync()
        {
            do
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Min(_step, _currentTime)),
                    delayTiming: PlayerLoopTiming.FixedUpdate, cancellationToken: _cts.Token);

                if (!_cts.IsCancellationRequested)
                {
                    _currentTime -= _step;
                    _invokeTick?.Invoke(_currentTime);
                }
            } while (_currentTime > 0f && !_cts.IsCancellationRequested);

            _active = false;

            if (_currentTime <= 0 && !_cts.IsCancellationRequested)
                _invokeComplete?.Invoke();

        }

    }
}