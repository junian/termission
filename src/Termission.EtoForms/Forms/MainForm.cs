using System;
using Eto.Drawing;
using Eto.Forms;
using Juniansoft.Termission.Core.Constants;
using Juniansoft.Termission.EtoForms.MenuCommands;
using Juniansoft.Termission.EtoForms.Resources;
using Juniansoft.Termission.Core.ViewModels;
using Juniansoft.Termission.EtoForms.Views;
using Juniansoft.MvvmReady;
using Juniansoft.Termission.Core;

namespace Juniansoft.Termission.EtoForms.Forms
{
    public class MainForm : Form
    {
        Eto.Forms.Command _menuItemNew;
        Eto.Forms.Command _menuItemOpen;
        Eto.Forms.Command _menuItemSave;
        Eto.Forms.Command _menuItemSaveAs;
        Eto.Forms.Command _menuItemExit;
        Eto.Forms.Command _menuItemHelp;
        Eto.Forms.Command _menuItemAbout;
        Eto.Forms.Command _menuItemPreferences;

        CheckCommand _menuItemAlwaysOnTop;

        private MainView _mainView;
        private MainViewModel _mainVm;


        public MainForm()
        {
            _mainView = ServiceLocator.Current.Get<MainView>();
            _mainVm = ServiceLocator.Current.Get<MainViewModel>();

            Title = CoreApp.AssemblyProduct;
            ClientSize = new Eto.Drawing.Size(720, 480);
            Style = EtoStyles.FormMain;

            if(!Platform.IsGtk)
                Icon = DesktopAppResources.DevAppIcon;
            
            Menu = BuildMenu();
            Content = _mainView;

            MainApplication.TrayIndicator = CreateTrayIndicator();
            MainApplication.TrayIndicator.Show();

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
            _menuItemHelp.DelegatedCommand = new RelayCommand<object>((_) => 
            {
                new HelpForm().ShowModal(this);
            });
        }

        private MenuBar BuildMenu()
        {
            if (!Platform.Supports<MenuBar>())
                return null;

            var menuBar = new MenuBar
            {
                AboutItem = _menuItemAbout = new AboutCommand()
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
            view.Items.Add(new Eto.Forms.Command
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

            //var help = menuBar.Items.GetSubmenu("He&lp");
            menuBar.HelpItems.Add((_menuItemHelp = new HelpCommand()));

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

            menuBar.ApplicationItems.Add((_menuItemPreferences = new PreferencesCommand
            {
                DelegatedCommand = new RelayCommand<object>((_) =>
                {
                    new PreferencesForm().ShowModal(this);
                }),
            }), 900);


            return menuBar;
        }

        private void ShowDeviceBot()
        {
            var botDialog = new DeviceBotForm();
            botDialog.ShowModal(this);
        }

        private TrayIndicator CreateTrayIndicator()
        {
            var tray = new TrayIndicator
            {
                Image = !Platform.IsGtk ? DesktopAppResources.DevAppIcon : DesktopAppResources.DevAppLogo,
                Title = CoreApp.AssemblyProduct
            };

            var menu = new ContextMenu();
            menu.Items.Add(_menuItemAbout);
            menu.Items.Add(_menuItemPreferences);
            menu.Items.Add(_menuItemExit);
            tray.Menu = menu;

            return tray;
        }
    }
}
