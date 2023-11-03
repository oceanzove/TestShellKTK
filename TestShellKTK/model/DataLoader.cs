using System.Collections.ObjectModel;
using Npgsql;

namespace TestShellKTK.model;

public class DataLoader
{
    public static ObservableCollection<User> Students { get; set; } = new ObservableCollection<User>();
    public static ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

    public static ObservableCollection<Role> Roles { get; set; } = new ObservableCollection<Role>();


    public static ObservableCollection<Role> LoadRoles()
    {
        Roles.Clear();
        NpgsqlCommand command = PostgresRepository.Command("Select role_name FROM role");
        NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string roleName = reader.GetString(0);
                
                Roles.Add(new Role(roleName));
            }
        }
        reader.Close();
        return Roles;
    }
    public static ObservableCollection<User> LoadUsers()
    {
        Students.Clear();
        NpgsqlCommand command = PostgresRepository.Command("Select username, \"password\", role.role_name From \"user\" " +
                                                           "INNER JOIN role ON \"user\".role = role.id ");
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string username = reader.GetString(0);
                string password = reader.GetString(1);
                string userRole = reader.GetString(2);
                
                Users.Add(new User(username, password, userRole));
            }
        }
        reader.Close();
        return Users;
    }
    
    public static ObservableCollection<User> LoadStudents()
    {
        Students.Clear();
        NpgsqlCommand command = PostgresRepository.Command("Select username, \"password\", role.role_name From \"user\" " +
                                                           "INNER JOIN role ON \"user\".role = role.id " +
                                                           "WHERE role.role_name = 'student'");
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string username = reader.GetString(0);
                string password = reader.GetString(1);
                string userRole = reader.GetString(2);
                
                Students.Add(new User(username, password, userRole));
            }
        }
        reader.Close();
        return Students;
    }
}