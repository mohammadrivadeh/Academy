using System.Configuration;
using System.Data;
using System.Windows;
using Telerik.Windows.Controls;

namespace AcademyEFCore_NET7
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            StyleManager.ApplicationTheme = new Windows11Theme();
            Windows11Palette.LoadPreset(Windows11Palette.ColorVariation.Dark);
            new Views.Home().Show();
            base.OnStartup(e);
        }
    }

}
