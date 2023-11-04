using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using NpgsqlTypes;
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

        // // Заполняет ComboBox Ролями
        var roles = new Binding();
        roles.Source = DataLoader.LoadRoles();
        cbUserRole.SetBinding(ItemsControl.ItemsSourceProperty, roles);
        
    }
    
    private void AddUser(object sender, RoutedEventArgs e)
    {
        //TODO сделай try_catch
        
        var Username = tbStudentUsername.Text.Trim();
        var Password = tbStudentPassword.Text.Trim();
        var selectedItem = cbUserRole.SelectedItem as Role;
        var UserRole = selectedItem.RoleName;
        
        // string UserRole = cbUserRole.SelectedItem.ToString();
        
        var command = PostgresRepository.Command("INSERT INTO \"user\" (username, password, role) " +
                                                 "VALUES (@username, @password, (SELECT id FROM role WHERE role_name = @UserRole))");
        command.Parameters.AddWithValue("@username", NpgsqlDbType.Varchar, Username);
        command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, Password);
        command.Parameters.AddWithValue("@UserRole", NpgsqlDbType.Varchar, UserRole);
        var result = command.ExecuteNonQuery();
        tbStudentUsername.Clear();
        tbStudentPassword.Clear();
        cbUserRole.SelectedIndex = -1;
        DataLoader.LoadStudents();
    }
    
    private void CurrentStudentEdit(object sender, SelectionChangedEventArgs e)
    {
        spStudentsEditor.IsEnabled = true;
    }
    
    
    private void SaveEditionsButton(object sender, RoutedEventArgs e)
    {
        spStudentsEditor.IsEnabled = false;
        try
        {
            var selectedUser = lbUsers.SelectedItem as User;
            int id = selectedUser.Id;
            string username = tbStudentUsernameEdit.Text.Trim();
            string password = tbStudentPasswordEdit.Text.Trim();

            NpgsqlCommand command = PostgresRepository.Command("UPDATE \"user\" SET username = @username,password = @password " +
                                                               "WHERE id = @id");
            command.Parameters.AddWithValue("@username", NpgsqlDbType.Varchar, username);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, password);
            
            int result = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("somme errors here " + ex.Message);
        }
        DataLoader.LoadStudents();
    }
}