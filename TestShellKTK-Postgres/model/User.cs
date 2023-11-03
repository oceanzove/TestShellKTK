using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestShellKTK.model;

public class User : INotifyPropertyChanged
{

    private string _Username;
    public string Username
    {
        get => _Username;
        set => SetField(ref _Username, value);
    }

    private string _Password;
    public string Password
    {
        get => _Password;
        set => SetField(ref _Password, value);
    }

    private string _UserRole;

    public string UserRole
    {
        get => _UserRole;
        set => SetField(ref _UserRole, value);
    }

    public User(string username, string password, string userRole)
    {
        _Username = username;
        _Password = password;
        _UserRole = userRole;
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