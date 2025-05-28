using Zenject;

namespace Game.Logic.Handlers.Factory
{
    public class HandlerFactory : IFactory
    {
        private readonly DiContainer _container;

        public HandlerFactory(DiContainer container) =>
            _container = container;

        public THandler Create<THandler>()
            where THandler : class, IHandler
        {
            THandler handler = _container.Instantiate<THandler>();
            return handler;
        }
    }
}