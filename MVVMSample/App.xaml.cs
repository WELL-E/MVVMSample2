using System.Windows;
using MVVMSample.ViewModels;

namespace MVVMSample
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var vw = new MainWindow();
            var vm = new MainWindowViewModel();
            vw.DataContext = vm;
            vw.Show();
        }
    }
}
