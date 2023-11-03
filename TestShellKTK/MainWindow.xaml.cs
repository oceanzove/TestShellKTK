using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using NpgsqlTypes;
using TestShellKTK.model;

namespace TestShellKTK;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //TODO передлать базу
    public MainWindow()
    {
        InitializeComponent();
        PostgresRepository.Connect("localhost", "5432", "postgres", "1234", "AnotherTestMasterDB");
        DataContext = this;

        // Заполняет ComboBox Ролями
        Binding roles = new Binding();
        roles.Source = DataLoader.LoadRoles();
        cbUserRole.SetBinding(ComboBox.ItemsSourceProperty, roles);
        
        
        // Заполняет ListBox пользвателями
        Binding students = new Binding();
        students.Source = DataLoader.LoadUsers();
        lbUsers.SetBinding(ListBox.ItemsSourceProperty, students);
    }


    private void AddUser(object sender, RoutedEventArgs e)
    {
        //TODO сделай try_catch
        
        string Username = tbStudentUsername.Text.Trim();
        string Password = tbStudentPassword.Text.Trim();
        Role selectedItem = cbUserRole.SelectedItem as Role;
        string UserRole = selectedItem.RoleName;
        
        // string UserRole = cbUserRole.SelectedItem.ToString();

        NpgsqlCommand command = PostgresRepository.Command("INSERT INTO \"user\" (username, password, role) " +
                                                           "VALUES (@username, @password, (SELECT id FROM role WHERE role_name = @UserRole))");
        command.Parameters.AddWithValue("@username", NpgsqlDbType.Varchar, Username);
        command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, Password);
        command.Parameters.AddWithValue("@UserRole", NpgsqlDbType.Varchar, UserRole);
        var result = command.ExecuteNonQuery();
        tbStudentUsername.Clear();
        tbStudentPassword.Clear();
        cbUserRole.SelectedIndex = -1;
        DataLoader.LoadUsers();
    }
}