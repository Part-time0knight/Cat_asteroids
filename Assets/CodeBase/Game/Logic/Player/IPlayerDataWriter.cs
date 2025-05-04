using UnityEngine;

namespace Game.Logic.Player
{
    public interface IPlayerPositionWriter
    {
        Vector2 Position { set; }
    }

    public interface IPlayerScoreWriter
    {
        int Score { set; }

        void AddScore(int score, Vector2 targetPosition);
    }

    public interface IPlayerHitsWriter
    {
        int Hits { set; get; }
        bool IsTakeDamage { set; }
    }
}