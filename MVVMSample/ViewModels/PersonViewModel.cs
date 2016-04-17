using System.Windows;
using MVVMSample.Comm;
using MVVMSample.Models;

namespace MVVMSample.ViewModels
{
    public class PersonViewModel : ViewModelBase
    {
        private readonly PersonModel _person;

        public PersonViewModel(PersonModel model)
        {
            _person = model;
        }

        public string Name
        {
            get { return _person.Name; }
            set
            {
                _person.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Sex
        {
            get { return _person.Sex; }
            set
            {
                _person.Sex = value; 
                OnPropertyChanged("Sex");
            }
        }

        public int Age
        {
            get { return _person.Age; }
            set
            {
                _person.Age = value;
                OnPropertyChanged("Age");
            }
        }

        /// <summary>
        /// 保存命令
        /// </summary>
        private DelegateCommand<Window> _saveCmd;

        public DelegateCommand<Window> SaveCmd
        {
            get
            {
                if (_saveCmd == null)
                {
                    _saveCmd = new DelegateCommand<Window>(InvokeSave, IsSave);
                }

                return _saveCmd;
            }
        }

        private void InvokeSave(Window win)
        {
            if (win == null) return;
            win.DialogResult = true;
            win.Close();
        }

        private bool IsSave(Window win)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            return true;
        }

    }
}
