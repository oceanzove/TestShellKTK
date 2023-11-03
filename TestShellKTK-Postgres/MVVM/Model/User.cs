using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestShellKTK.model;

public class User : INotifyPropertyChanged
{
    private string _Password;

    private string _Username;

    private string _UserRole;

    public User(string username, string password, string userRole)
    {
        _Username = username;
        _Password = password;
        _UserRole = userRole;
    }

    public string Username
    {
        get => _Username;
        set => SetField(ref _Username, value);
    }

    public string Password
    {
        get => _Password;
        set => SetField(ref _Password, value);
    }

    public string UserRole
    {
        get => _UserRole;
        set => SetField(ref _UserRole, value);
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}