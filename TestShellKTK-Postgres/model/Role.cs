using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestShellKTK.model;

public class Role : INotifyPropertyChanged
{
    private string _RoleName;
    public string RoleName
    {
        get => _RoleName;
        set => SetField(ref _RoleName, value);
    }

    public Role(string roleName)
    {
        _RoleName = roleName;
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