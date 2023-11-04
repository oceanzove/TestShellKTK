using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestShellKTK.model;

public class User : INotifyPropertyChanged
{
    private int _id;
    
    private string _password;

    private string _username;

    private string _fullName;

    private string _userRole;

    public User(int id, string username, string fullName, string password, string userRole)
    {
        _id = id;
        _username = username;
        _fullName = fullName;
        _password = password;
        _userRole = userRole;
    }

    public int id
    {
        get => _id;
    }
    public string username
    {
        get => _username;
        set => SetField(ref _username, value);
    }

    public string fullName
    {
        get => _fullName;
        set => SetField(ref _fullName, value);
    }

    public string password
    {
        get => _password;
        set => SetField(ref _password, value);
    }

    public string userRole
    {
        get => _userRole;
        set => SetField(ref _userRole, value);
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