using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NpgsqlTypes;
using TestShellKTK.model;

namespace TestShellKTK;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        PostgresRepository.Connect("localhost", "5432", "postgres", "1234", "TestMasterDB");
        
    }

    private void onClickShutdown(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}