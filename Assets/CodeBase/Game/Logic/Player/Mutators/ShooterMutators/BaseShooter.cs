using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player.Mutators.ShooterMutators
{
    public class BaseShooter
    {
        private readonly IHandlerSetter _handlerSetter;


        public BaseShooter(IHandlerSetter handlerSetter)
        {
            _handlerSetter = handlerSetter;

        }

        public void Set()
        {
            _handlerSetter.Set<PlayerBaseShootHandler, IPlayerShootHandler>();
        }
    }
}