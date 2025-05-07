using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Handlers
{
    public class PauseInputHandler : ITickable
    {
        public event Action OnPressPause;

        public PauseInputHandler()
        {
        }

        public void Tick()
        {
            if (Input.GetButtonDown("Cancel"))
                OnPressPause?.Invoke();
        }

    }
}