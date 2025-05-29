
namespace Game.Logic.Handlers
{
    public interface IWeaponHandler : IHandler
    {
        void TickableDamage(UnitFacade target);
    }
}