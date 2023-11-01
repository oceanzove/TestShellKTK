using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestShellKTK.model;

public class Student : INotifyPropertyChanged
{

    private string _username;
    public string Username
    {
        get => _username;
        set => SetField(ref _username, value);
    }

    private string _password;
    public string Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }

    private string _NameClass;

    public string NameClass
    {
        get => _NameClass;
        set => SetField(ref _NameClass, value);
    }

    public Student(string username, string password, string nameClass)
    {
        Username = username;
        Password = password;
        NameClass = nameClass;
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