namespace TestShellKTK.model;

public class GeneratePassword
{
    private static Random random = new Random();
    
    public static string GetPassword(int length)
    {
        
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}