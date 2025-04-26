
using System.Collections.Generic;

namespace Game.Logic.Handlers
{
    public class PauseHandler : IPauseHandler
    {

        private bool _isPaused;

        private List<IPauseble> _pausebleObjects;

        public bool Active

        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                _pausebleObjects.ForEach((item) => item.OnPause(value));
            }
        }

        public PauseHandler()
        {
            _isPaused = false;
            _pausebleObjects = new();
        }

        public bool Get()
            => _isPaused;

        public void Set(bool active)
        {
            _isPaused = active;
        }

        public void SubscribeElement(IPauseble pausebleObject)
        {
            if (_pausebleObjects.Contains(pausebleObject))
                return;
            _pausebleObjects.Add(pausebleObject);
            pausebleObject.OnPause(_isPaused);
        }

        public void UnsubscribeElement(IPauseble pausebleObject)
        {
            if (!_pausebleObjects.Contains(pausebleObject))
                return;
            _pausebleObjects.Remove(pausebleObject);
        }
    }
}