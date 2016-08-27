using System.Windows;

namespace DML_MobgameClient.UI
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var app = new MainWindow();
            var context = new MainWindowViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}
