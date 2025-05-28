
namespace Game.Logic.Handlers.Strategy
{
    public interface IHandlerSetter
    {

        public void Set<ClassTHandler, InterfaceTHandler>()
            where ClassTHandler : class, IHandler
            where InterfaceTHandler : class, IHandler;
    }
}