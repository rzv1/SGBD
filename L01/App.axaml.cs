using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using L01.Models;
using L01.ViewModels;
using L01.Views;

namespace L01;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            string connectionString = "Host=192.168.2.45;Port=5432;Database=gym;Username=postgres;Password=rzv";
            DatabaseManager manager = new DatabaseManager(connectionString);
            CustomerRepo cRepo = new CustomerRepo(manager);
            PaymentRepo pRepo = new PaymentRepo(manager);
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(cRepo, pRepo),
            };
            //Inchidem conextiunea cu baza de date la terminarea aplicatiei
            //manager.CloseConnection();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}