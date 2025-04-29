

using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerData : IPlayerDataReader, IPlayerDataWriter
    {
        private int _hits;
        private Vector2 _position;

        public Vector2 Position 
        { 
            get => _position;
            set => _position = value;
        }
    }
}
