using NpgsqlTypes;

namespace TestShellKTK.model;

public class GenerateUsername
{
    public static string GetUsername(string fullName)
    {
        var words = fullName.Split(' ');

        var username = words[0].ToLower() + new Random().Next(1000, 9999).ToString();

        var command = PostgresRepository.Command("SELECT COUNT(*) FROM \"user\" WHERE username = @username");
        command.Parameters.AddWithValue("@username", NpgsqlDbType.Varchar, username);
        var reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                if (reader.GetInt32(0) > 0)
                {
                    GetUsername(fullName);
                }
            }
        }
        reader.Close();
        return username;
    }
}

