using System;
using System.Windows.Input;
using Eto.Forms;
using Juniansoft.Termission.Core.Constants;
using Juniansoft.Termission.EtoForms.MenuCommands;
using Juniansoft.Termission.EtoForms.Resources;
using Juniansoft.Termission.Core.ViewModels;
using Juniansoft.Termission.EtoForms.Views;
using Juniansoft.MvvmReady;

namespace Juniansoft.Termission.EtoForms.Forms
{
    public class DeviceBotForm : Dialog
    {
        private DeviceBotViewModel _botVM;
        private MainViewModel _mainVm;
        private DeviceBotView _botView;

        private CheckCommand _enableCommand;
        private Eto.Forms.Command _compileCommand;
        private Eto.Forms.Command _newCommand;
        private Eto.Forms.Command _openCommand;
        private Eto.Forms.Command _saveCommand;
        private Eto.Forms.Command _saveAsCommand;

        public DeviceBotForm()
        {
            _mainVm = ServiceLocator.Current.Get<MainViewModel>();
            _botVM = ServiceLocator.Current.Get<DeviceBotViewModel>();
            _botView = ServiceLocator.Current.Get<DeviceBotView>();

            Style = EtoStyles.DeviceBotDialog;
            Resizable = true;
            Minimizable = true;
            Maximizable = true;
            Content = _botView;
            ToolBar = new ToolBar
            {
                Style = EtoStyles.Toolbar,
                Items =
                {
                    (_newCommand = new NewCommand{}),
                    (_openCommand = new OpenCommand{}),
                    (_saveCommand = new SaveCommand{}),
                    (_saveAsCommand = new SaveAsCommand{}),
                    new SeparatorToolItem{ Type = SeparatorToolItemType.FlexibleSpace, },
                    (_enableCommand = new BotActiveCommand{}),
                    (_compileCommand = new CompileCommand{}),
                },
            };

            for (var i = 0; i < 4; i++)
            {
                ToolBar.Items[i].Style = EtoStyles.ButtonToolItem;
            }

            this.Closing += (_, e) =>
            {
                if (this.DataContext is DeviceBotViewModel vm)
                {
                    if (vm.IsBotEnabled == true)
                    {
                        var err = vm.Compile();
                        if (string.IsNullOrEmpty(err))
                        {
                            _mainVm.DeviceBotEngine = vm.DeviceBotEngine;
                        }
                        else
                        {
                            MessageBox.Show(this, $"{err}", "Compile Error!", MessageBoxType.Error);
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            };

            _botVM.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(DeviceBotViewModel.IsBotEnabled))
                {
                    //_compileCommand.Enabled = _botVM.IsBotEnabled;
                }
            };

            if (!Platform.IsWpf)
                this.BindDataContext(x => x.Title, Binding.Property((DeviceBotViewModel vm) => vm.Title).Convert(x => x ?? " ", x => x ?? " "), DualBindingMode.OneWay);
            this.BindDataContext(x => x.Width, Binding.Property((DeviceBotViewModel vm) => vm.Width));
            this.BindDataContext(x => x.Height, Binding.Property((DeviceBotViewModel vm) => vm.Height));

            _enableCommand.BindDataContext(x => x.Checked, Binding.Property((DeviceBotViewModel vm) => vm.IsBotEnabled));
            //_compileCommand.BindDataContext(x => x.Enabled, Binding.Property((DeviceBotViewModel vm) => vm.IsBotEnabled));

            _compileCommand.BindDataContext(x => x.DelegatedCommand, Binding.Property((DeviceBotViewModel vm) => vm.CompileCommand));
            _newCommand.BindDataContext(x => x.DelegatedCommand, Binding.Property((DeviceBotViewModel vm) => vm.NewCommand));
            _openCommand.BindDataContext(x => x.DelegatedCommand, Binding.Property((DeviceBotViewModel vm) => vm.OpenCommand));
            _saveCommand.BindDataContext(x => x.DelegatedCommand, Binding.Property((DeviceBotViewModel vm) => vm.SaveCommand));
            _saveAsCommand.BindDataContext(x => x.DelegatedCommand, Binding.Property((DeviceBotViewModel vm) => vm.SaveAsCommand));

            this.DataContext = _botVM;
            _compileCommand.DataContext = _botVM;
            _enableCommand.DataContext = _botVM;
            _newCommand.DataContext = _botVM;
            _openCommand.DataContext = _botVM;
            _saveCommand.DataContext = _botVM;
            _saveAsCommand.DataContext = _botVM;
        }
    }
}
