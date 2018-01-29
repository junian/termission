using System;
using Eto.Drawing;
using Eto.Forms;
using Juniansoft.Samariterm.Core.Constants;
using Juniansoft.Samariterm.EtoForms.MenuCommands;
using Juniansoft.Samariterm.EtoForms.Resources;
using Juniansoft.Samariterm.Core.ViewModels;
using Juniansoft.Samariterm.EtoForms.Views;

namespace Juniansoft.Samariterm.EtoForms.Forms
{
    public class MainForm : Form
    {
        Command _menuItemNew;
        Command _menuItemOpen;
        Command _menuItemSave;
        Command _menuItemSaveAs;
        Command _menuItemExit;

        CheckCommand _menuItemAlwaysOnTop;

        private MainView _mainView;
        private MainViewModel _mainVm;


        public MainForm()
        {
            _mainView = Core.ServiceLocator.Instance.Get<MainView>();
            _mainVm = Core.ServiceLocator.Instance.Get<MainViewModel>();

            Title = MainApplication.AssemblyProduct;
            ClientSize = new Eto.Drawing.Size(720, 480);
            Style = EtoStyles.FormMain;
            Menu = BuildMenu();
            Content = _mainView;

            ConfigureDataBinding();

            this.DataContext = _mainVm;
            _menuItemAlwaysOnTop.DataContext = _mainVm;
            _menuItemNew.DataContext = _mainVm;
            _menuItemOpen.DataContext = _mainVm;
            _menuItemSave.DataContext = _mainVm;
            _menuItemSaveAs.DataContext = _mainVm;
            _menuItemExit.DataContext = _mainVm;
        }

        private void ConfigureDataBinding()
        {
            this.BindDataContext(x => x.Topmost, Binding.Property((MainViewModel vm) => vm.IsTopMost), DualBindingMode.OneWay);
            if (!Platform.IsWpf)
                this.BindDataContext(x => x.Title, Binding.Property((MainViewModel vm) => vm.Title), DualBindingMode.OneWay);
            this._menuItemAlwaysOnTop.BindDataContext(x => x.Checked, Binding.Property((MainViewModel vm) => vm.IsTopMost));
            this._menuItemNew.BindDataContext(x => x.DelegatedCommand, Binding.Property((MainViewModel vm) => vm.NewCommand));
            this._menuItemOpen.BindDataContext(x => x.DelegatedCommand, Binding.Property((MainViewModel vm) => vm.OpenCommand));
            this._menuItemSave.BindDataContext(x => x.DelegatedCommand, Binding.Property((MainViewModel vm) => vm.SaveCommand));
            this._menuItemSaveAs.BindDataContext(x => x.DelegatedCommand, Binding.Property((MainViewModel vm) => vm.SaveAsCommand));
            this._menuItemExit.BindDataContext(x => x.DelegatedCommand, Binding.Property((MainViewModel vm) => vm.ExitCommand));
        }

        private MenuBar BuildMenu()
        {
            if (!Platform.Supports<MenuBar>())
                return null;

            var menuBar = new MenuBar
            {
                AboutItem = new AboutCommand()
                {
                    DelegatedCommand = new RelayCommand<object>((_) =>
                    {
                        new AboutForm().ShowModal(this);
                    }),
                },
                QuitItem = _menuItemExit = new QuitCommand(),
            };

            var file = menuBar.Items.GetSubmenu("&File");
            file.Items.Add((_menuItemNew = new NewCommand()));
            file.Items.Add((_menuItemOpen = new OpenCommand()));
            file.Items.Add((_menuItemSave = new SaveCommand()));
            file.Items.Add((_menuItemSaveAs = new SaveAsCommand()));
            //file.Items.Add(new SeparatorMenuItem());

            var view = menuBar.Items.GetSubmenu("&View");
            view.Items.Add(new Command
            {
                MenuText = "Device Bot",
                DelegatedCommand = new RelayCommand<object>((_) =>
                {
                    ShowDeviceBot();
                }),
            });
            view.Items.Add((_menuItemAlwaysOnTop = new CheckCommand
            {
                MenuText = "Always on top",
            }));

            if (Platform.IsMac || Platform.IsWpf || Platform.IsWinForms)
            {
                menuBar.ApplicationItems.Add(new UpdateCommand
                {
                    DelegatedCommand = new RelayCommand<object>((_) =>
                    {
                        new PreferencesForm().ShowModal(this);
                    }),
                });
            }

            menuBar.ApplicationItems.Add(new PreferencesCommand
            {
                DelegatedCommand = new RelayCommand<object>((_) =>
                {
                    new PreferencesForm().ShowModal(this);
                }),
            }, 900);


            return menuBar;
        }

        private void ShowDeviceBot()
        {
            var botDialog = new DeviceBotForm();
            botDialog.ShowModal(this);
        }
    }
}
