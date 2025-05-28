
namespace Game.Logic.Handlers.Strategy
{
    public interface IHandlerSetter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ClassTHandler">Implementation type</typeparam>
        /// <typeparam name="InterfaceTHandler">Key type</typeparam>
        public void Set<ClassTHandler, InterfaceTHandler>()
            where ClassTHandler : class, IHandler
            where InterfaceTHandler : class, IHandler;
    }
}