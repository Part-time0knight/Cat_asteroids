using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerDynamicData : IPlayerPositionReader, 
        IPlayerHitsReader,
        IPlayerScoreReader,
        IPlayerPositionWriter,
        IPlayerHitsWriter,
        IPlayerScoreWriter
    {
        public event Action<bool> OnMove;

        public event Action OnScoreUpdate;
        public event Action<int, Vector2> OnScoreAdd;
        public event Action<bool> OnDamaged;
        public event Action OnHitsUpdate;


        private Vector2 _position;
        private int _score;
        private int _hits;

        public Vector2 Position 
        { 
            get => _position;
            set => _position = value;
        }

        public int Score
        {
            get => _score;
            set 
            { 
                _score = value;
                OnScoreUpdate?.Invoke();
            }
        }

        public int Hits
        {
            get => _hits;
            set 
            {
                _hits = value;
                OnHitsUpdate?.Invoke();
            }
        }

        public bool IsTakeDamage { set => OnDamaged?.Invoke(value); }
        public bool MakeMove { set => OnMove?.Invoke(value); }

        public void AddScore(int score, Vector2 targetPosition)
        {
            Score += score;
            OnScoreAdd?.Invoke(score, targetPosition);
        }
    }
}
