using Game.Logic.Player;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Handlers
{
    public class DifficultHandler : IInitializable, IDisposable
    {
        public event Action<int> OnDifficultUpdate;

        private readonly IPlayerScoreReader _playerScoreReader;
        private readonly Settings _settings;

        private int _currentDifficult = 0;

        public int CurrentDifficult => _currentDifficult;

        public DifficultHandler(IPlayerScoreReader playerScoreReader,
            Settings settings)
        {
            _playerScoreReader = playerScoreReader;
            _settings = settings;
        }

        public void Initialize()
        {
            _playerScoreReader.OnScoreUpdate += InvokeScoreUpdate;
        }

        public void Dispose()
        {
            _playerScoreReader.OnScoreUpdate -= InvokeScoreUpdate;
        }

        private int GetDifficult()
        {
            int difficult = 0,
                score = _playerScoreReader.Score,
                temp = _settings.StepScore;

            while (temp <= score)
            {
                temp = Mathf.RoundToInt(temp * _settings.Multiplier);
                difficult++;
            }
            return difficult;
        }

        private void InvokeScoreUpdate()
        {
            int difficult = GetDifficult();
            if (difficult == _currentDifficult)
                return;
            _currentDifficult = difficult;
            OnDifficultUpdate?.Invoke(_currentDifficult);

        }


        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int StepScore;
            [field: SerializeField] public float Multiplier;
        }
    }
}