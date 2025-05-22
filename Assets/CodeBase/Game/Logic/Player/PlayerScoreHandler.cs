using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerScoreHandler : IPlayerScoreReader,
        IPlayerScoreWriter,
        IInitializable,
        IScoreSave
    {
        public const string LoadKey = "Score";

        public event Action OnScoreUpdate;
        public event Action<int, Vector2> OnScoreAdd;

        private int _score;
        private int _maxScore;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                if (_score > _maxScore)
                    _maxScore = _score;
                OnScoreUpdate?.Invoke();
            }
        }

        public int MaxScore => _maxScore;

        public void AddScore(int score, Vector2 targetPosition)
        {
            Score += score;
            OnScoreAdd?.Invoke(score, targetPosition);
        }

        public void Save()
        {

            PlayerPrefs.SetInt(LoadKey, _maxScore);
            PlayerPrefs.Save();
        }

        private void Load()
        {
            _maxScore = PlayerPrefs.GetInt(LoadKey, 0);
        }

        public void Initialize()
        {
            Load();
        }
    }
}