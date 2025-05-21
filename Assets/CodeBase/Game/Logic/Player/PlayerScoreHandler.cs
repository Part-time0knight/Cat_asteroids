using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerScoreHandler : IPlayerScoreReader, IPlayerScoreWriter
    {
        public event Action OnScoreUpdate;
        public event Action<int, Vector2> OnScoreAdd;

        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreUpdate?.Invoke();
            }
        }

        public void AddScore(int score, Vector2 targetPosition)
        {
            Score += score;
            OnScoreAdd?.Invoke(score, targetPosition);
        }

    }
}