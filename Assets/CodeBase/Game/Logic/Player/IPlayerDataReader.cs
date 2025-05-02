using UnityEngine;

namespace Game.Logic.Player
{
    public interface IPlayerPositionReader
    {
        Vector2 Position { get; }
    }

    public interface IPlayerScoreReader
    {
        int Score { get; }
    }

    public interface IPlayerHitsReader
    {
        int Hits { get; }
    }
}