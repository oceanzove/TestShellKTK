using System.Collections.ObjectModel;
using Npgsql;

namespace TestShellKTK.model;

public class DataLoader
{
    public static ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

    public static void LoadStudents()
    {
        Students.Clear();
        NpgsqlCommand command = PostgresRepository.Command("Select username, password, class.name " +
                                                           "From student " +
                                                           "INNER JOIN class ON class_id = class.id;");
        NpgsqlDataReader result = command.ExecuteReader();
        if (result.HasRows)
        {
            while (result.Read())
            {
                string username = result.GetString(0);
                string password = result.GetString(1);
                string nameClass = result.GetString(2);
                
                Students.Add(new Student(username, password, nameClass));
            }
        }
        result.Close();
    }
}