using Game.Logic.Handlers;

namespace Game.Logic.Player.Handlers
{
    public interface IPlayerMoveHandler : IMoveHandler
    {
        void Move();

        void ReverseMove();

        void Rotate(float horizontal);
    }
}