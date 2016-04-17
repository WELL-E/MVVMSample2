using System.ComponentModel;

namespace MVVMSample.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string protertyName)
        {
            var handler = this.PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(protertyName));
        }
    }
}
