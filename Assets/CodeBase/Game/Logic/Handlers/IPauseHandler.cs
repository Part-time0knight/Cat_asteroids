namespace Game.Logic.Handlers
{
    public interface IPauseHandler
    {
        bool Active { get; set; }

        void SubscribeElement(IPauseble pausebleObject);

        void UnsubscribeElement(IPauseble pausebleObject);

    }
}