using UnityEngine;

namespace Game.Logic.Handlers.Factory
{
    public interface IFactory
    {
        THandler Create<THandler>()
            where THandler : class, IHandler;
    }
}