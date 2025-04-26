using Game.Logic.Handlers;
using UnityEngine;
using Zenject;

public class PauseTest : MonoBehaviour
{
    private IPauseHandler _player;

    [Inject]
    private void Construct(IPauseHandler player)
    {
        _player = player;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _player.Active = !_player.Active;
    }
}
