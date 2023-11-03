using TestShellKTK.Core;

namespace TestShellKTK.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
}