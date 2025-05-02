using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerData : IPlayerPositionReader, 
        IPlayerHitsReader,
        IPlayerScoreReader,
        IPlayerPositionWriter,
        IPlayerHitsWriter,
        IPlayerScoreWriter
    {

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
            set => _score = value;
        }

        public int Hits
        {
            get => _hits;
            set => _hits = value;
        }
    }
}
