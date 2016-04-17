using System;
using System.Windows;
using System.Windows.Controls;
using MVVMSample.Comm;
using MVVMSample.Models;
using MVVMSample.ViewModels;

namespace MVVMSample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : IView
    {
        private MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            this.LvPersons.SelectionChanged += LvPersons_OnSelectionChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_vm == null)
            {
                _vm = this.DataContext as MainWindowViewModel;
                if (_vm != null)
                {
                    _vm.View = this;
                }
            }
        }

        private void LvPersons_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = this.LvPersons.SelectedItem as PersonModel;
            if (item != null)
            {
                _vm.PersonProfile = string.Format("姓名：{0}, 性别：{1}, 年龄：{2};", item.Name, item.Sex, item.Age);
            }
        }

        public void SetAddress(Uri uri)
        {
            this.Wb.Navigate(uri);
        }
    }
}
