﻿using System.Collections.ObjectModel;

namespace TestShellKTK.model;

public class DataLoader
{
    public static ObservableCollection<User> Students { get; set; } = new();

    public static ObservableCollection<User> Teachers { get; set; } = new();
    public static ObservableCollection<User> Users { get; set; } = new();

    public static ObservableCollection<Role> Roles { get; set; } = new();


    public static ObservableCollection<Role> LoadRoles()
    {
        Roles.Clear();
        var command = PostgresRepository.Command("Select role_name FROM role");
        var reader = command.ExecuteReader();

        if (reader.HasRows)
            while (reader.Read())
            {
                var roleName = reader.GetString(0);

                Roles.Add(new Role(roleName));
            }

        reader.Close();
        return Roles;
    }

    // public static ObservableCollection<User> LoadUsers()
    // {
    //     Users.Clear();
    //     var command = PostgresRepository.Command("Select username, \"password\", role.role_name From \"user\" " +
    //                                              "INNER JOIN role ON \"user\".role = role.id ");
    //     var reader = command.ExecuteReader();
    //     if (reader.HasRows)
    //         while (reader.Read())
    //         {
    //             var username = reader.GetString(0);
    //             var password = reader.GetString(1);
    //             var userRole = reader.GetString(2);
    //
    //             Users.Add(new User(username, password, userRole));
    //         }
    //
    //     reader.Close();
    //     return Users;
    // }

    public static ObservableCollection<User> LoadStudents()
    {
        Students.Clear();
        var command = PostgresRepository.Command("Select \"user\".id, username, \"password\", role.role_name From \"user\" " +
                                                 "INNER JOIN role ON \"user\".role = role.id " +
                                                 "WHERE role.role_name = 'student'");
        var reader = command.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
            {
                var id = reader.GetInt16(0);
                var username = reader.GetString(1);
                var password = reader.GetString(2);
                var userRole = reader.GetString(3);

                Students.Add(new User(id, username, password, userRole));
            }

        reader.Close();
        return Students;
    }

    public static ObservableCollection<User> LoadTeachers()
    {
        Teachers.Clear();
        var command = PostgresRepository.Command("Select \"user\".id, username, \"password\", role.role_name From \"user\" " +
                                                 "INNER JOIN role ON \"user\".role = role.id " +
                                                 "WHERE role.role_name = 'teacher'");
    
        var reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                var id = reader.GetInt16(0);
                var username = reader.GetString(1);
                var password = reader.GetString(2);
                var userRole = reader.GetString(3);
    
                Teachers.Add(new User(id, username, password, userRole));
            }
        }
    
        reader.Close();
        return Teachers;
    }
}