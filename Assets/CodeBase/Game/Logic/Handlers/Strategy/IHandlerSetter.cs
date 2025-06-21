
namespace Game.Logic.Handlers.Strategy
{
    public interface IHandlerSetter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ClassTHandler">Implementation type</typeparam>
        /// <typeparam name="KeyTHandler">Key type</typeparam>
        public void Set<ClassTHandler, KeyTHandler>()
            where ClassTHandler : class, IHandler
            where KeyTHandler : IHandler;
    }
}