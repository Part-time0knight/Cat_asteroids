
namespace Game.Logic.Handlers.Strategy
{
    public interface IHandlerGetter
    {
        public THandler Get<THandler>()
            where THandler : class, IHandler;
    }
}