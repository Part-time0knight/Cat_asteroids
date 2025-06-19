
namespace Game.Logic.Player.Mutators.VampiricMutator
{
    public interface IVampiricWriter
    {
        int NeedPoints { set; }

        int CurrentPoints { set; }

        bool Reload { set; }
    }
}