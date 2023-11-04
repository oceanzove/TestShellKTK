using System.Windows.Controls;
using System.Windows.Data;
using TestShellKTK.model;

namespace TestShellKTK.MVVM.View;

public partial class StudentView : UserControl
{
    public StudentView()
    {
        InitializeComponent();
        
        // Заполняет ListBox пользвателями
        var students = new Binding();
        students.Source = DataLoader.LoadStudents();
        lbUsers.SetBinding(ItemsControl.ItemsSourceProperty, students);
    }
}