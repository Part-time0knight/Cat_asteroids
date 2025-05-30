
namespace Game.Logic.Handlers
{
    public interface IWeaponHandler : IHandler
    {
        void FrameDamage(UnitFacade target);
    }
}