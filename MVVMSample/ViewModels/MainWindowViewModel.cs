using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MVVMSample.Comm;
using MVVMSample.Models;
using MVVMSample.Views;

namespace MVVMSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IView View { private get; set; }

        public MainWindowViewModel()
        {
            Persons.Add(new PersonModel { Id = 1, Name = "Charles", Age = 20, Sex = "男" });
            Persons.Add(new PersonModel { Id = 2, Name = "Wendy", Age = 20, Sex = "女" });
        }

        /// <summary>
        /// 人员列表
        /// </summary>
        private ObservableCollection<PersonModel> _persons;

        public ObservableCollection<PersonModel> Persons
        {
            get
            {
                if (_persons == null)
                {
                    _persons = new ObservableCollection<PersonModel>();
                }

                return _persons;
            }
            set
            {
                _persons = value;
            }
        }

        /// <summary>
        /// 人员信息
        /// </summary>
        private string _personPofile;

        public string PersonProfile
        {
            get { return _personPofile; }
            set
            {
                _personPofile = value;
                OnPropertyChanged("PersonProfile");
            }
        }

        /// <summary>
        /// 添加命令
        /// </summary>
        private ICommand _addPersonCmd;

        public ICommand AddPersonCmd
        {
            get
            {
                if (_addPersonCmd == null)
                {
                    _addPersonCmd = new DelegateCommand(()=>
                    {
                        var model = new PersonModel();
                        var vm = new PersonViewModel(model);
                        var vw = new PersonView { DataContext = vm };
                        vw.ShowDialog();
                        if (!vw.DialogResult.Equals(true)) return;
                        Persons.Add(model);
                    });
                }

                return _addPersonCmd;
            }
        }


        /// <summary>
        /// 跳转命令
        /// </summary>
        private ICommand _navigationCmd;

        public ICommand NavigationCmd
        {
            get
            {
                if (_navigationCmd == null)
                {
                    _navigationCmd = new DelegateCommand(() =>
                    {
                        if (View != null)
                        {
                            View.SetAddress(new Uri("http://www.cnblogs.com"));
                        }
                    });
                }
                return _navigationCmd;
            }
        }

    }
}
