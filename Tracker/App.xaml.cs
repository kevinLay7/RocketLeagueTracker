using ControlzEx.Theming;
using System.Windows;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var settings = AppSettings.Load();

            if(settings != null)
            {
                //Set theme
                ThemeManager.Current.ChangeThemeColorScheme(this, settings.Color);        
                ThemeManager.Current.ChangeThemeBaseColor(this, settings.UseDarkMode.Value ? "Dark" : "Light");
            }

            var window = new MainWindow(settings);
            window.Show();
        }
    }
}
