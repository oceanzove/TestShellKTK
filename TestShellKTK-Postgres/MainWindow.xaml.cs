using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NpgsqlTypes;
using TestShellKTK.model;

namespace TestShellKTK;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        PostgresRepository.Connect("localhost", "5432", "postgres", "1234", "AnotherTestMasterDB");
        DataContext = this;
        //
        // // Заполняет ComboBox Ролями
        // var roles = new Binding();
        // roles.Source = DataLoader.LoadRoles();
        // cbUserRole.SetBinding(ItemsControl.ItemsSourceProperty, roles);
        //
        //
        // // Заполняет ListBox пользвателями
        // var students = new Binding();
        // students.Source = DataLoader.LoadUsers();
        // lbUsers.SetBinding(ItemsControl.ItemsSourceProperty, students);
    }


    private void AddUser(object sender, RoutedEventArgs e)
    {
        // //TODO сделай try_catch
        //
        // var Username = tbStudentUsername.Text.Trim();
        // var Password = tbStudentPassword.Text.Trim();
        // var selectedItem = cbUserRole.SelectedItem as Role;
        // var UserRole = selectedItem.RoleName;
        //
        // // string UserRole = cbUserRole.SelectedItem.ToString();
        //
        // var command = PostgresRepository.Command("INSERT INTO \"user\" (username, password, role) " +
        //                                          "VALUES (@username, @password, (SELECT id FROM role WHERE role_name = @UserRole))");
        // command.Parameters.AddWithValue("@username", NpgsqlDbType.Varchar, Username);
        // command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, Password);
        // command.Parameters.AddWithValue("@UserRole", NpgsqlDbType.Varchar, UserRole);
        // var result = command.ExecuteNonQuery();
        // tbStudentUsername.Clear();
        // tbStudentPassword.Clear();
        // cbUserRole.SelectedIndex = -1;
        // DataLoader.LoadUsers();
    }

    private void onClickShutdown(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}