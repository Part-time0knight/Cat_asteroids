
namespace Game.Logic.Player.Mutators.GyperjumpMutator
{
    public interface IGyperjumpWriter
    {
        float ReloadTime { set; }

        bool Reload { set; }

        bool Pause { set; }

        void SetReload();

        void SetEndReload();
    }
}