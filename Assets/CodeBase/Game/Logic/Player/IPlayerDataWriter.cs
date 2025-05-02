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
    }

    public interface IPlayerHitsWriter
    {
        int Hits { set; }
    }
}