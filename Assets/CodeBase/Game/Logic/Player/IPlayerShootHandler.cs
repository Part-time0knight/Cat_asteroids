
namespace Game.Logic.Player
{
    public interface IPlayerShootHandler
    {
        public bool Active { set; }
        public bool Pause { set; }

        void Clear();
    }
}