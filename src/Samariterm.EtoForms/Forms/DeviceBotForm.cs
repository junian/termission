using System;
using System.Windows.Input;
using Eto.Forms;
using Juniansoft.Samariterm.Core.Constants;
using Juniansoft.Samariterm.EtoForms.MenuCommands;
using Juniansoft.Samariterm.EtoForms.Resources;
using Juniansoft.Samariterm.Core.ViewModels;
using Juniansoft.Samariterm.EtoForms.Views;

namespace Juniansoft.Samariterm.EtoForms.Forms
{
    public class DeviceBotForm : Dialog
    {
        private DeviceBotViewModel _botVM;
        private MainViewModel _mainVm;
        private DeviceBotView _botView;

        private CheckCommand _enableCommand;
        private Command _compileCommand;
        private Command _newCommand;
        private Command _openCommand;
        private Command _saveCommand;
        private Command _saveAsCommand;

        public DeviceBotForm()
        {
            _mainVm = Core.ServiceLocator.Instance.Get<MainViewModel>();
            _botVM = Core.ServiceLocator.Instance.Get<DeviceBotViewModel>();
            _botView = Core.ServiceLocator.Instance.Get<DeviceBotView>();

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
