using Core.MVVM.ViewModel;
using Core.MVVM.Windows;

public class GameplayViewModel : AbstractViewModel
{
    public GameplayViewModel(IWindowFsm windowFsm) : base(windowFsm)
    {
    }

    public override void InvokeClose()
    {
        _windowFsm.CloseWindow();
    }

    public override void InvokeOpen()
    {
        _windowFsm.OpenWindow(Window, inHistory: true);
    }
}
