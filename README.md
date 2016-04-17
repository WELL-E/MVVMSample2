## 我们都想追求完美 ##

> Every view in the app has an empty codebehind file, except for the standard boilerplate code that calls InitializeComponent in the class's constructor. In fact, you could remove the views' codebehind files from the project and the application would still compile and run correctly

Josh Smith说可以删除views' codebehind文件，程序也能正常运行。

啊...还有这么完美的事情？那是不是views' codebehind文件里一行代码都不写，所有的代码都可以写在ViewModel里就可以了，那这样显示层和业务逻辑层就可以完美分离了？

理想是丰满的，现实却是骨干的啊！为了追求极致的MVVM，在实际项目开发中会把自己搞的非常纠结，不知从何下手...

比如使用的第三方控件，它不支持依赖项属性，那我们只能把代码写在views' codebehind文件里。问题来了，这个控件要使用ViewModel里的逻辑，那*View怎么访问ViewModel的方法呢*？ViewModel里要获取这个控件的数据，那*ViewModel如何调用View里的方法呢*？搜了一通，发现这个问题不好搞...

这个“老鼠屎”控件破坏了我们实现完美MVVM的计划，哎呀，这时候开始后悔使用MVVM了，后悔使用WPF了，干脆使用WinForm方式开发算了，或者找个第三方MVVM框架，看能不能解决...我当初就有这种心理。

下面通过一个简单的demo来演示，如何解决上面的两个问题：

![](http://i.imgur.com/S1r1F4S.png)

## View中使用ViewModel

我们在程序启动的时候已经使用了相应的ViewModel初始化了View的DataContext

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
    
 因此在View中，我们可以通过DataContext获取ViewModel
 
	private MainWindowViewModel _vm;

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
	    if (_vm == null)
	    {
	        _vm = this.DataContext as MainWindowViewModel;
	        ...
	    }
	}
	
在View中使用ViewModel中的属性或方法

	private void LvPersons_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
	    var item = this.LvPersons.SelectedItem as PersonModel;
	    if (item != null)
	    {
	        _vm.PersonProfile = string.Format("姓名：{0}, 性别：{1}, 年龄：{2};", item.Name, item.Sex, item.Age);
	    }
	}
	
	注：可以使用blend提供的System.Window.interactivity插件，将View中事件转换为ViewModel中命令

## ViewModel中使用View

先定义一个接口

	namespace MVVMSample.Comm
	{
		public interface IView
		{
		   void SetAddress(Uri uri);
		}
	}

View中初始化并实现这个接口

	public partial class MainWindow : IView
	{
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
        
	   ...
	
	    public void SetAddress(Uri uri)
	    {
	        this.Wb.Navigate(uri);
	    }
	}

ViewModel中定义并使用View中的接口

	public IView View { private get; set; }
	
	...
	
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
	                	//IView中接口方法
	                    View.SetAddress(new Uri("http://www.bing.com"));
	                }
	            });
	        }
	        return _navigationCmd;
	    }
	}
	
问题总算解决了，关键是，使用上述方法还能够进行单元测试。

## 总结

跟业务逻辑相关的操作一定要放到ViewModel中，跟业务逻辑无关的可以放到Views' Codebehind文件里。MVVM模式是开发中的指导原则，不能强套，需要灵活使用。
