using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public interface IPlayerPositionReader
    {
        Vector2 Position { get; }
    }

    public interface IPlayerScoreReader
    {
        event Action OnScoreUpdate;
        event Action<int, Vector2> OnScoreAdd;

        int Score { get; }
    }

    public interface IPlayerHitsReader
    {
        event Action OnHitsUpdate;
        int Hits { get; }
    }
}