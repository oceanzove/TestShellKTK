using Npgsql;

namespace TestShellKTK;

public class PostgresRepository
{
    private static NpgsqlConnection connection;

    public static void Connect(string host, string port, string user, string pass, string dbname)
    {
        var connectionString = string.Format("Server={0};Port={1};User ID={2};Password={3};DataBase={4}", host, port, user, pass, dbname);

        connection = new NpgsqlConnection(connectionString);
        connection.Open();
    }

    public static NpgsqlCommand Command(string sql)
    {
        var command = new NpgsqlCommand();
        command.Connection = connection;
        command.CommandText = sql;
        return command;
    }
}