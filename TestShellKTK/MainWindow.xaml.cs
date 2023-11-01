using System.Collections.ObjectModel;
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
    public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

    public MainWindow()
    {
        InitializeComponent();
        PostgresRepository.Connect("localhost", "5432", "postgres", "1234", "TestMasterDB");
        DataContext = this;

        DataLoader.LoadStudents();





    }

    

    private void AddStudent(object sender, RoutedEventArgs e)
    {
        //TODO сделай try_catch
        
        
        string StudentUsername = tbStudentUsername.Text.Trim();
        string StudentPassword = tbStudentPassword.Text.Trim();
        string StudentNameClass = tbStudentNameClass.Text.Trim();

        NpgsqlCommand command = PostgresRepository.Command("INSERT INTO student (username, password, class_id) " +
                                                           "VALUES (@username, @password, (SELECT id FROM class WHERE name = @nameClass))");
        command.Parameters.AddWithValue("@username", NpgsqlDbType.Varchar, StudentUsername);
        command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, StudentPassword);
        command.Parameters.AddWithValue("@nameClass", NpgsqlDbType.Varchar, StudentNameClass);
        var result = command.ExecuteNonQuery();
        tbStudentUsername.Clear();
        tbStudentPassword.Clear();
        tbStudentNameClass.Clear();
        DataLoader.LoadStudents();
    }
}