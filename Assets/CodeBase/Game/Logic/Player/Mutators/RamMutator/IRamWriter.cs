using UnityEngine;

namespace Game.Logic.Player.Mutators.RamMutator
{
    public interface IRamWriter
    {
        float ReloadTime { set; }

        bool Reload { set; }

        bool Pause { set; }

    }
}