
using Game.Logic.Handlers;

namespace Game.Logic.Player.Handlers
{
    public interface IPlayerShootHandler : IShootHandler
    {
        public bool Active { set; }
        public bool IsPause { set; }

        void Clear();
    }
}