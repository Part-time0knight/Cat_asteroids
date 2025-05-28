using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player
{
    public interface IPlayerMoveHandler : IMoveHandler
    {
        event Action<float> OnHaste;

        void Move();

        void ReverseMove();

        void Rotate(float horizontal);
    }
}