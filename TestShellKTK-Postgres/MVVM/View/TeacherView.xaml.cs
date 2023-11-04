using System.Windows.Controls;
using System.Windows.Data;
using TestShellKTK.model;

namespace TestShellKTK.MVVM.View;

public partial class TeacherView : UserControl
{
    public TeacherView()
    {
        InitializeComponent();

        Binding teachers = new Binding();
        teachers.Source = DataLoader.LoadTeachers();
        lbUsers.SetBinding(ListBox.ItemsSourceProperty, teachers);
    }
}