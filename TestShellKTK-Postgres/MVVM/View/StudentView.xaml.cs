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

        
    }
    
    private void AddStudent(object sender, RoutedEventArgs e)
    {
        //TODO сделай try_catch
        //TODO сделай валидацию даных проверку на заполненость фио
        var fullName = tbStudentFullname.Text.Trim();
        var username = GenerateUsername.GetUsername(fullName);
        var password = GeneratePassword.GetPassword(10);
        var role = "student";
        
        var command = PostgresRepository.Command("INSERT INTO \"user\" (username, fullname, password, role) " +
                                                 "VALUES (@username, @fullName, @password, (SELECT id FROM role WHERE role_name = @UserRole))");
        command.Parameters.AddWithValue("@username", NpgsqlDbType.Varchar, username);
        command.Parameters.AddWithValue("@fullName", NpgsqlDbType.Varchar, fullName);
        command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, password);
        command.Parameters.AddWithValue("@UserRole", NpgsqlDbType.Varchar, role);
        var result = command.ExecuteNonQuery();
        tbStudentFullname.Clear();
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
            int id = selectedUser.id;
            string fullName = tbStudentFullNameEdit.Text.Trim();

            NpgsqlCommand command = PostgresRepository.Command("UPDATE \"user\" SET fullname = @fullName WHERE id = @id");
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            command.Parameters.AddWithValue("@fullName", NpgsqlDbType.Varchar, fullName);
            
            int result = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("somme errors here " + ex.Message);
        }
        DataLoader.LoadStudents();
    }
}