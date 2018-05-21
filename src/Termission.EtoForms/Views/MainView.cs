using System;
using System.IO.Ports;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Juniansoft.Termission.Core.Constants;
using Juniansoft.Termission.EtoForms.Controls;
using Juniansoft.Termission.EtoForms.Resources;
using Juniansoft.Termission.Core.ViewModels;

namespace Juniansoft.Termission.EtoForms.Views
{
    public class MainView : Panel
    {
        private DropDown _dropDownSerialPort;

        private ComboBox _dropDownBaudRate;

        private EnumDropDown<Handshake> _dropDownHandshake;
        private EnumDropDown<Parity> _dropDownParity;

        private DropDown _radDataBits;
        private EnumDropDown<StopBits> _radStopBits;
        private RadioButtonList _radActivityMode;

        private CheckBox _chkIsCR;
        private CheckBox _chkIsLF;

        private Button _btnRefresh;
        private Button _btnSend;
        private Button _btnTestBot;
        private Button _btnRun;

        private Label _lblConnected;

        private Container _grpDeviceSetting;
        private Container _grpDeviceActivities;

        private TextArea _textSendCommand;
        private TextArea _textLog;

        public MainView()
        {
            Content = BuildContent();

            ConfigureEvents();
            ConfigureDataBindings();

            this.Load += (_, e) => _textLog.Focus();

            this.DataContextChanged += (_, e) =>
            {
                if (this.DataContext is MainViewModel vm)
                {
                    vm.LogReceived += (__, ev) =>
                    {
                        Application.Instance.Invoke(() =>
                        {
                            _textLog.Append($"[{ev.TimeStamp.ToString()}] {ev.LogLevel} {ev.Message.TrimEnd('\r', '\n')}{Environment.NewLine}", true);
                        });
                    };
                }
            };
        }

        private Control BuildContent()
        {
            return new StackLayout
            {
                Spacing = 10,
                Padding = 5,
                Orientation = Orientation.Vertical,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Items =
                {
                    new StackLayoutItem
                    {
                        Control = _grpDeviceSetting = GroupDeviceSettings(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                    },
                    new StackLayoutItem
                    {
                        Control = GroupDeviceSelection(),
                        HorizontalAlignment = HorizontalAlignment.Left,
                    },
                    new StackLayoutItem
                    {
                        Control = _grpDeviceActivities = GroupDeviceActivities(),
                        Expand = true,
                    },
                }
            };
        }

        private Container GroupDeviceSelection()
        {
            return new Panel
            {
                //Text = "Device Selection",
                Content = new StackLayout
                {
                    Orientation = Orientation.Horizontal,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    //Padding = 5,
                    Spacing = 5,
                    Items =
                    {
                        new Label{ Text = "Device:", Visible = false },
                        (_dropDownSerialPort = new DropDown{ Width = 256, }),
                        (_btnRefresh = new Button{ Text="Refresh", }),
                        (_btnRun = new Button{ Text = "Run" }),
                        new StackLayoutItem
                        {
                            Control = (_lblConnected = new Label
                            {
                                TextAlignment = TextAlignment.Center,
                                Font = SystemFonts.Bold(),
                                Visible = false,
                            }),
                            Expand = true,
                        },
                    },
                },
            };
        }

        private Container GroupDeviceSettings()
        {
            return new Panel
            {
                Visible = false,
                //Text = "Device Settings",
                Content = new TableLayout
                {
                    //Padding = 5,
                    Spacing = new Size(5, 5),
                    Rows =
                    {
                        new TableRow
                        {
                            Cells =
                            {
                                new Label{Text="Baud Rate", TextAlignment = TextAlignment.Center},
                                new Label{Text="Handshake", TextAlignment = TextAlignment.Center},
                                new Label{Text="Parity", TextAlignment = TextAlignment.Center},
                                new Label{Text="Data Bits", TextAlignment = TextAlignment.Center},
                                new Label{Text="Stop Bits", TextAlignment = TextAlignment.Center},
                                null,
                            },
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                (_dropDownBaudRate = new ComboBox{}),
                                (_dropDownHandshake = new EnumDropDown<Handshake>{}),
                                (_dropDownParity = new EnumDropDown<Parity>{}),
                                (_radDataBits = new DropDown {}),
                                (_radStopBits = new EnumDropDown<StopBits>{}),
                                null,
                            },
                        },
                        null,
                    }
                }
            };
        }

        private Container GroupDeviceActivities()
        {
            return new Panel
            {
                //Text = "Device Activities",
                Content = new StackLayout
                {
                    Padding = 5,
                    Spacing = 5,
                    Orientation = Orientation.Vertical,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Items =
                    {
                        new StackLayout
                        {
                            Spacing = 5,
                            Orientation = Orientation.Horizontal,
                            Items =
                            {
                                (_radActivityMode = new RadioButtonList {}),
                                (_chkIsCR = new CheckBox{Text="CR | 0x0D"}),
                                (_chkIsLF = new CheckBox{Text="LF | 0x0A"}),
                            }
                        },

                        new StackLayout
                        {
                            Orientation=Orientation.Horizontal,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            Items =
                            {
                                new StackLayoutItem
                                {
                                    Control = _textSendCommand = new TextArea
                                    {
                                        Style = EtoStyles.SendCommandText,
                                        BackgroundColor = Colors.White
                                    },
                                    Expand = true,
                                },
                                new StackLayout
                                {
                                    Orientation = Orientation.Vertical,
                                    VerticalContentAlignment = VerticalAlignment.Center,
                                    Padding = 5,
                                    Spacing = 5,
                                    Items =
                                    {
                                        new StackLayoutItem
                                        {
                                            Control = _btnSend = new Button{Text = "Send"},
                                            Expand = true,
                                        },
                                        new StackLayoutItem
                                        {
                                            Control = _btnTestBot = new Button{Text = "Test Bot"},
                                            Expand = true,
                                        },
                                    },
                                },
                            },
                        },

                        new StackLayoutItem
                        {
                            Control = _textLog = new TextArea
                            {
                                Style = EtoStyles.SendCommandText,
                                BackgroundColor = Colors.DimGray,
                                TextColor = Colors.White,
                                SpellCheck =false,
                                Wrap = true,
                                ReadOnly = true
                            },
                            Expand = true,
                        },
                    },
                },
            };
        }

        private void ConfigureEvents()
        {
            _dropDownBaudRate.KeyDown += (_, e) =>
            {
                var handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
                if (Platform.IsMac)
                {
                    // Arrow key on macOS
                    handled &= !(e.KeyChar >= 63232 && e.KeyChar <= 63235);
                }
                e.Handled = handled;
            };

            if (Platform.IsMac)
            {
                _grpDeviceSetting.EnabledChanged += OnContainerEnabledChanged;
                _grpDeviceActivities.EnabledChanged += OnContainerEnabledChanged;
            }

            _btnSend.Click += (_, e) =>
            {
                _textSendCommand.Focus();
                _textSendCommand.SelectAll();
            };
        }

        private void ConfigureDataBindings()
        {
            _btnSend.BindDataContext(x => x.Command, (MainViewModel vm) => vm.SendCommand);

            _grpDeviceSetting.BindDataContext(x => x.Enabled, Binding.Property((MainViewModel vm) => vm.IsRunning).Convert(x => !x), DualBindingMode.OneWay);

            //_radActivityMode.BindDataContext(x => x.Enabled, (MainViewModel vm) => vm.IsRunning, DualBindingMode.OneWay);
            //_chkIsCR.BindDataContext(x => x.Enabled, (MainViewModel vm) => vm.IsRunning, DualBindingMode.OneWay);
            //_chkIsLF.BindDataContext(x => x.Enabled, (MainViewModel vm) => vm.IsRunning, DualBindingMode.OneWay);
            //_textSendCommand.BindDataContext(x => x.Enabled, (MainViewModel vm) => vm.IsRunning, DualBindingMode.OneWay);
            _btnSend.BindDataContext(x => x.Enabled, (MainViewModel vm) => vm.IsRunning, DualBindingMode.OneWay);
            _btnTestBot.BindDataContext(x => x.Enabled, (MainViewModel vm) => vm.IsRunning, DualBindingMode.OneWay);

            _dropDownSerialPort.BindDataContext(
                c => c.DataStore,
                Binding.Property((MainViewModel vm) => vm.SerialPortList).Convert(x => x.Cast<object>()));
            _dropDownSerialPort.SelectedValueBinding.BindDataContext(
                (MainViewModel m) => m.SelectedSerialPortName);
            //_dropDownSerialPort.SelectedIndexBinding.BindDataContext(
            //    (MainViewModel m) => m.SelectedSerialPortIndex);
            _dropDownSerialPort.BindDataContext(
                c => c.Enabled,
                Binding.Property((MainViewModel vm) => vm.IsRunning).Convert(x => !x));

            _dropDownBaudRate.BindDataContext(
                c => c.DataStore,
                Binding.Property((MainViewModel vm) => vm.BaudRateOptions).Convert(x => x.Cast<object>()));
            _dropDownBaudRate.SelectedValueBinding.BindDataContext(
                (MainViewModel m) => m.SelectedBaudRate);

            _dropDownHandshake.SelectedValueBinding.BindDataContext(
                Binding.Property((MainViewModel m) => m.SelectedHandshake)
                .Convert(x => (Handshake) x, x => (int) x));

            _dropDownParity.SelectedValueBinding.BindDataContext(
                Binding.Property((MainViewModel m) => m.SelectedParity)
                .Convert(x => (Parity)x, x => (int)x));

            _radDataBits.BindDataContext(
                c => c.DataStore,
                Binding.Property((MainViewModel vm) => vm.DataBitsOptions).Convert(x => x.Cast<object>()));
            _radDataBits.SelectedValueBinding.BindDataContext(
                (MainViewModel m) => m.SelectedDataBits);

            //_radStopBits.BindDataContext(
            //c => c.DataStore,
            //Binding.Property((MainViewModel vm) => vm.StopBitsOptions).Convert(x => x.Cast<object>()));
            _radStopBits.SelectedValueBinding.BindDataContext(
                Binding.Property((MainViewModel m) => m.SelectedStopBits)
                .Convert(x => (StopBits)x, x => (int)x));

            _radActivityMode.BindDataContext(
                c => c.DataStore,
                Binding.Property((MainViewModel vm) => vm.ActivityModeOptions).Convert(x => x.Cast<object>()));
            _radActivityMode.SelectedValueBinding.BindDataContext(
                (MainViewModel m) => m.SelectedActivityMode);

            _chkIsCR.CheckedBinding.BindDataContext(
                (MainViewModel m) => m.IsCR);
            _chkIsLF.CheckedBinding.BindDataContext(
                (MainViewModel m) => m.IsLF);

            _btnRun.BindDataContext(x => x.Text, (MainViewModel vm) => vm.RunText);
            _btnRun.BindDataContext(x => x.Command, (MainViewModel vm) => vm.RunCommand);
            _btnRun.BindDataContext(x => x.Enabled, (MainViewModel vm) => vm.CanRun);

            _lblConnected.BindDataContext(x => x.Text, (MainViewModel vm) => vm.ConnectedText);

            _btnRefresh.BindDataContext(x => x.Command, (MainViewModel vm) => vm.RefreshCommand);
            _btnRefresh.BindDataContext(x => x.Enabled, Binding.Property((MainViewModel vm) => vm.IsRunning).Convert(x => !x), DualBindingMode.OneWay);

            _textSendCommand.BindDataContext(x => x.Text, (MainViewModel vm) => vm.SendCommandText);
        }

        private void OnContainerEnabledChanged(object sender, EventArgs e)
        {
            if (sender is Container gb)
            {
                foreach (var c in gb.Children)
                {
                    c.Enabled = gb.Enabled;
                }
            }
        }
    }
}
