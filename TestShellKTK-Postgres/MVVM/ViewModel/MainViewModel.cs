using TestShellKTK.Core;

namespace TestShellKTK.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    
    public RelayCommand StudentViewCommand { get; set; }
    public RelayCommand TeacherViewCommand { get; set; }
    public RelayCommand HeadViewCommand { get; set; }
    public StudentViewModel StudentVM { get; set; }
    public TeacherViewModel TeacherVM { get; set; }
    public HeadViewModel HeadVM { get; set; }
    
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

    public MainViewModel()
    {
        StudentVM = new StudentViewModel();
        TeacherVM = new TeacherViewModel();
        HeadVM = new HeadViewModel();
        CurrentView = TeacherVM;

        StudentViewCommand = new RelayCommand((O => CurrentView = StudentVM));
        TeacherViewCommand = new RelayCommand(o => CurrentView = TeacherVM);
        HeadViewCommand = new RelayCommand(o => CurrentView = HeadVM);
    }
}