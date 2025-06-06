using Game.Logic.Handlers;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using Game.Logic.Projectiles;
using Zenject;

namespace Game.Logic.Player.Mutators
{
    public class BaseMutator : IInitializable
    {
        private readonly IHandlerSetter _handlerSetter;


        public BaseMutator(IHandlerSetter handlerSetter)
        {
            _handlerSetter = handlerSetter;

        }

        public void Initialize()
        {
            Resolve();
        }

        private void Resolve()
        {
            _handlerSetter.Set<PlayerInvincibilityHandler, IInvincibilityHandler>();
            
            _handlerSetter.Set<PlayerBaseMoveHandler, IPlayerMoveHandler>();
            _handlerSetter.Set<PlayerDamageHandler, IPlayerDamageHandler>();
            _handlerSetter.Set<PlayerWeaponHandler, IWeaponHandler>();
            _handlerSetter.Set<PlayerInputHandler, IInputHandler>();
        }
    }
}