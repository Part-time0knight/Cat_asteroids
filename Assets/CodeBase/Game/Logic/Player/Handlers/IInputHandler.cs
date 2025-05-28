using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player.Handlers
{
    public interface IInputHandler : IHandler
    {
        event Action InvokeMoveButtonsDown;

        event Action InvokeMoveButtonsUp;

        event Action<float> InvokeMoveHorizontal;

        event Action<float> InvokeMoveVertical;
    }
}