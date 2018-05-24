using Juniansoft.MvvmReady;
using Juniansoft.Termission.Core;
using Juniansoft.Termission.Core.Services;
using Juniansoft.Termission.Core.ViewModels;
using Juniansoft.Termission.Mobile.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Juniansoft.Termission.Mobile
{
    public partial class App : Application, ITermissionApp
    {
        public App()
        {
            InitializeComponent();

            RegisterServices();

            MainPage = new AboutPage
            {
                //BindingContext = ServiceLocator.Instance.Get<DeviceBotViewModel>(),
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void RegisterServices()
        {
            // Register all Services here
            //ServiceLocator.Instance.Register<ICrossDialog, CrossDialog>();
            //ServiceLocator.Instance.Register<IJavaScriptBotEngine, JSJintScriptEngine>();
            //ServiceLocator.Instance.Register<INetworkEngine, TermSharpEngine>();
            //ServiceLocator.Instance.Register<ISystemService, EtoSystemService>();
            //ServiceLocator.Instance.Register<IFileService, FileService>();
            ServiceLocator.Current.Register<IJsonService, LitJsonService>();

            //if (Platform.Instance.IsWinForms || Platform.IsWpf)
            //ServiceLocator.Instance.Register<ICSharpBotEngine, CSharpCodeDomScriptEngine>();

            // Register all ViewModels here.
            ServiceLocator.Current.Register<MainViewModel>();
            ServiceLocator.Current.Register<DeviceBotViewModel>();
            ServiceLocator.Current.Register<PreferencesViewModel>();

            // Register all Views here
            //ServiceLocator.Instance.Register<MainView>();
            //ServiceLocator.Instance.Register<DeviceBotView>();
            //ServiceLocator.Instance.Register<PreferencesView>();
        }

        public void Run(string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}
