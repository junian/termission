using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Juniansoft.Termission.Core.Constants;
using Juniansoft.Termission.EtoForms.Controls;
using Juniansoft.Termission.Core.Extensions;
using Juniansoft.Termission.Core.ViewModels;
using static Juniansoft.Termission.EtoForms.Controls.SyntaxHightlightTextArea;
using System.Diagnostics;

namespace Juniansoft.Termission.EtoForms.Views
{
    public class DeviceBotView : Panel
    {
        private CheckBox _chkEnableBot;
        private Label _lblCompileStatus;
        private Button _btnCompile;
        private Button _btnNew;
        private Button _btnOpen;
        private Button _btnSave;
        private Button _btnSaveAs;
        private SyntaxHightlightTextArea _textSourceCode;
        private RadioButtonList _radBotLanguages;

        public DeviceBotView()
        {
            this.Content = BuildContent();
            _textSourceCode.TextChanged += (_, e) =>
            {
                if (this.DataContext is DeviceBotViewModel vm)
                {
                    vm.IsCompiled = false;
                    vm.IsCompileStatusVisible = false;
                }
            };

            this.DataContextChanged += (_, e) =>
            {
                if (this.DataContext is DeviceBotViewModel vm)
                {
                    vm.CompileFinishedAction = (err) =>
                    {
                        if (string.IsNullOrEmpty(err))
                            MessageBox.Show(this, "Your script successfully compiled! Now you can use it to work.", "Compile Success!", MessageBoxType.Information);
                        else
                        {
                            var errMsg = err.Truncate(512, "...");
                            if (Platform.IsGtk)
                            {
                                errMsg = errMsg.Replace("{", "{{").Replace("}", "}}");
                            }
                            MessageBox.Show(this, @errMsg, "Compile Error!", MessageBoxType.Error);
                        }
                    };
                }
            };

            ConfigureDataBinding();
        }

        private Control BuildContent()
        {
            return new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Items =
                {
                    new StackLayout
                    {
                        Visible = false,
                        Padding = 5,
                        Spacing = 5,
                        Orientation = Orientation.Horizontal,
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Items =
                        {
                            new StackLayoutItem
                            {
                                Control = _chkEnableBot = new CheckBox{ Text = "Enable Bot" },
                                HorizontalAlignment = HorizontalAlignment.Left,
                            },

                            new StackLayoutItem
                            {
                                Control = new Label{Text = "   "},
                            },

                            new StackLayoutItem
                            {
                                Control = _btnCompile = new Button{ Text = "Compile" },
                                HorizontalAlignment = HorizontalAlignment.Right,
                            },

                            new StackLayoutItem
                            {
                                Control = _lblCompileStatus = new Label{ Text = "success", TextColor = Colors.Green },
                                HorizontalAlignment = HorizontalAlignment.Right,
                            },

                            new StackLayoutItem
                            {
                                Control = new Label{Text = "   "},
                            },

                            new StackLayoutItem
                            {
                                Control = _btnNew = new Button{ Text = "New" },
                            },
                            new StackLayoutItem
                            {
                                Control = _btnOpen = new Button{ Text = "Open" },
                            },
                            new StackLayoutItem
                            {
                                Control = _btnSave = new Button{ Text = "Save" },
                            },
                            new StackLayoutItem
                            {
                                Control = _btnSaveAs = new Button{ Text = "Save As" },
                            },
                        },
                    },
                    new StackLayoutItem
                    {
                        Control = new Panel
                        {
                            Content = (_radBotLanguages = new RadioButtonList {}),
                            Padding = 5,
                        }
                    },
                    new StackLayoutItem
                    {
                        Control = _textSourceCode = new SyntaxHightlightTextArea{ Style = EtoStyles.SourceEditor, },
                        Expand = true,
                    },
                    //new StackLayoutItem
                    //{
                    //    Control = new AceSourceEditor(),
                    //    Expand = true,
                    //},
                },
            };
        }

        private void ConfigureDataBinding()
        {
            _btnCompile.BindDataContext(x => x.Command, Binding.Property((DeviceBotViewModel vm) => vm.CompileCommand));

            _btnNew.BindDataContext(x => x.Command, Binding.Property((DeviceBotViewModel vm) => vm.NewCommand));
            _btnOpen.BindDataContext(x => x.Command, Binding.Property((DeviceBotViewModel vm) => vm.OpenCommand));
            _btnSave.BindDataContext(x => x.Command, Binding.Property((DeviceBotViewModel vm) => vm.SaveCommand));
            _btnSaveAs.BindDataContext(x => x.Command, Binding.Property((DeviceBotViewModel vm) => vm.SaveAsCommand));

            _lblCompileStatus.BindDataContext(x => x.Visible, Binding.Property((DeviceBotViewModel vm) => vm.IsCompileStatusVisible), DualBindingMode.OneWay);

            _chkEnableBot.CheckedBinding.BindDataContext((DeviceBotViewModel vm) => vm.IsBotEnabled);

            _btnCompile.BindDataContext(x => x.Enabled, Binding.Property((DeviceBotViewModel vm) => vm.IsBotEnabled), DualBindingMode.OneWay);

            _textSourceCode.BindDataContext(x => x.Text, Binding.Property((DeviceBotViewModel vm) => vm.UserScript));
            _textSourceCode.BindDataContext(x => x.CodeLanguage, Binding.Property((DeviceBotViewModel vm) => vm.CodeLanguage), DualBindingMode.OneWay);

            _radBotLanguages.BindDataContext(
                c => c.DataStore,
                Binding.Property((DeviceBotViewModel vm) => vm.BotLanguages).Convert(x => x.Cast<object>()));
            _radBotLanguages.SelectedValueBinding.BindDataContext(
                (DeviceBotViewModel m) => m.SelectedBotLanguage);

            //_textSourceCode.BindDataContext(x => x.Enabled, Binding.Property((DeviceBotViewModel vm) => vm.IsBotEnabled), DualBindingMode.OneWay);
        }
    }
}
