using Game.Presentation.ViewModel;
using UnityEngine;
using Zenject;

public class MutatorCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private GameplayViewModel _gameplayViewModel;


    [Inject]
    private void Construct(GameplayViewModel gameplayViewModel)
    {
        _gameplayViewModel = gameplayViewModel;
        _gameplayViewModel.InvokedOpen += InvokeOpen;
        _gameplayViewModel.InvokedClose += InvokeClose;
    }

    private void InvokeOpen()
    {
        _canvas.enabled = true;
    }

    private void InvokeClose()
    {
        _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        _gameplayViewModel.InvokedOpen -= InvokeOpen;
        _gameplayViewModel.InvokedClose -= InvokeClose;
    }
}
