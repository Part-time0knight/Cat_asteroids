using System;
using System.Collections.Generic;
using Zenject;
using IFactory = Game.Logic.Handlers.Factory.IFactory;

namespace Game.Logic.Handlers.Strategy
{
    public class HandlerStrategy : IHandlerSetter,
        IHandlerGetter, IDisposable,
        IFixedTickable, ITickable,
        ILateTickable
    {
        private readonly IFactory _factory;

        private readonly Dictionary<Type, IHandler> _handlers = new();

        public HandlerStrategy(IFactory factory) 
        {
            _factory = factory;
        }

        public void Set<ClassTHandler, KeyTHandler>()
            where ClassTHandler : class, IHandler
            where KeyTHandler : IHandler
        {
            var handler = _factory.Create<ClassTHandler>();
            Initialize(handler);

            if (!_handlers.ContainsKey(typeof(KeyTHandler)))
                _handlers.Add(typeof(KeyTHandler), null);
            else
                if (_handlers[typeof(KeyTHandler)] is IDisposable disposable)
                    disposable.Dispose();

            _handlers[typeof(KeyTHandler)] = handler;
        }

        public THandler Get<THandler>()
            where THandler : class, IHandler
        {
            return _handlers[typeof(THandler)] as THandler;
        }

        public void Dispose()
        {
            foreach(var handler in _handlers.Values)
            {
                if (handler is IDisposable disposable)
                    disposable.Dispose();
            }    
        }

        public void FixedTick()
        {
            foreach (var handler in _handlers.Values)
            {
                if (handler is IFixedTickable tickable)
                    tickable.FixedTick();
            }
        }

        public void Tick()
        {
            foreach (var handler in _handlers.Values)
            {
                if (handler is ITickable tickable)
                    tickable.Tick();
            }
        }

        public void LateTick()
        {
            foreach (var handler in _handlers.Values)
            {
                if (handler is ILateTickable tickable)
                    tickable.LateTick();
            }
        }

        private void Initialize(IHandler handler)
        {
            if (handler is IInitializable initializable)
                initializable.Initialize();
        }
    }
}