using Eto.Drawing;
using Eto.Forms;
using Juniansoft.Termission.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juniansoft.Termission.EtoForms.Views
{
    public class PreferencesView : Panel
    {
        TextBox _textTcpListenerIp;
        NumericMaskedTextBox<int> _textTcpListenerPort;

        TextBox _textTcpClientIp;
        NumericMaskedTextBox<int> _textTcpClientPort;

        private ComboBox _dropDownBaudRate;

        private EnumDropDown<Handshake> _dropDownHandshake;
        private EnumDropDown<Parity> _dropDownParity;

        private DropDown _radDataBits;
        private EnumDropDown<StopBits> _radStopBits;

        private CheckBox _chkDtr;
        private CheckBox _chkRts;

        private Button _btnResetTcpListener;
        private Button _btnResetTcpClient;
        private Button _btnResetSerial;

        public PreferencesView()
        {
            Padding = 10;
            Content = BuildContent();

            ConfigureBinding();
        }

        private void ConfigureBinding()
        {
            _textTcpListenerIp.BindDataContext(x => x.Text, (PreferencesViewModel vm) => vm.TcpListenerIp);
            _textTcpListenerPort.BindDataContext(x => x.Value, (PreferencesViewModel vm) => vm.TcpListenerPort);

            _textTcpClientIp.BindDataContext(x => x.Text, (PreferencesViewModel vm) => vm.TcpClientIp);
            _textTcpClientPort.BindDataContext(x => x.Value, (PreferencesViewModel vm) => vm.TcpClientPort);

            _dropDownBaudRate.BindDataContext(
                c => c.DataStore,
                Binding.Property((PreferencesViewModel vm) => vm.BaudRateOptions).Convert(x => x.Cast<object>()));
            _dropDownBaudRate.SelectedValueBinding.BindDataContext(
                (PreferencesViewModel m) => m.SelectedBaudRate);

            _dropDownHandshake.SelectedValueBinding.BindDataContext(
                Binding.Property((PreferencesViewModel m) => m.SelectedHandshake)
                .Convert(x => (Handshake)x, x => (int)x));

            _dropDownParity.SelectedValueBinding.BindDataContext(
                Binding.Property((PreferencesViewModel m) => m.SelectedParity)
                .Convert(x => (Parity)x, x => (int)x));

            _radDataBits.BindDataContext(
                c => c.DataStore,
                Binding.Property((PreferencesViewModel vm) => vm.DataBitsOptions).Convert(x => x.Cast<object>()));
            _radDataBits.SelectedValueBinding.BindDataContext(
                (PreferencesViewModel m) => m.SelectedDataBits);

            //_radStopBits.BindDataContext(
            //c => c.DataStore,
            //Binding.Property((MainViewModel vm) => vm.StopBitsOptions).Convert(x => x.Cast<object>()));
            _radStopBits.SelectedValueBinding.BindDataContext(
                Binding.Property((PreferencesViewModel m) => m.SelectedStopBits)
                .Convert(x => (StopBits)x, x => (int)x));

            _chkDtr.CheckedBinding.BindDataContext(
                Binding.Property((PreferencesViewModel m) => m.IsDtrEnable)
                .Convert(x => (bool?)x, x => x == true));

            _chkRts.CheckedBinding.BindDataContext(
                Binding.Property((PreferencesViewModel m) => m.IsRtsEnable)
                .Convert(x => (bool?)x, x => x == true));

            _btnResetTcpListener.BindDataContext(c => c.Command, (PreferencesViewModel vm) => vm.ResetTcpListenerCommand);
            _btnResetTcpClient.BindDataContext(c => c.Command, (PreferencesViewModel vm) => vm.ResetTcpClientCommand);
            _btnResetSerial.BindDataContext(c => c.Command, (PreferencesViewModel vm) => vm.ResetSerialComCommand);
        }

        Control BuildContent()
        {
            return new TableLayout
            {
                Spacing = new Eto.Drawing.Size(5, 5),
                Rows =
                {
                    new TableRow(new Label{ Text = "TCP Listener Mode", Font = SystemFonts.Bold(), }),
                    new TableRow(new Label{ Text ="Ip Address:" }, (_textTcpListenerIp = new TextBox{ PlaceholderText="e.g. 127.0.0.1" })),
                    new TableRow(new Label{ Text="Port:" }, (_textTcpListenerPort = new NumericMaskedTextBox<int>{ PlaceholderText = "e.g. 8080" })),
                    new TableRow((_btnResetTcpListener = new Button { Text="Reset TCP Listener" }), null),
                    new TableRow(),
                    new TableRow(new Label{ Text = "TCP Client Mode", Font = SystemFonts.Bold(), }),
                    new TableRow(new Label{ Text ="Ip Address:" }, (_textTcpClientIp = new TextBox{ PlaceholderText="e.g. 127.0.0.1" })),
                    new TableRow(new Label{ Text="Port:" }, (_textTcpClientPort = new NumericMaskedTextBox<int>{ PlaceholderText = "e.g. 8080" })),
                    new TableRow((_btnResetTcpClient = new Button { Text="Reset TCP Client" }), null),
                    new TableRow(),
                    new TableRow( new Label { Text = "Serial COM Mode", Font = SystemFonts.Bold(), } ),
                    new TableRow(new Label{Text="Baud Rate" }, (_dropDownBaudRate = new ComboBox{})),
                    new TableRow(new Label{Text="Handshake" }, (_dropDownHandshake = new EnumDropDown<Handshake>{})),
                    new TableRow(new Label{Text="Parity" }, (_dropDownParity = new EnumDropDown<Parity>{})),
                    new TableRow(new Label{Text="Data Bits" }, (_radDataBits = new DropDown {})),
                    new TableRow(new Label{Text="Stop Bits" },(_radStopBits = new EnumDropDown<StopBits>{})),
                    new TableRow(new Label{Text="Enable Dtr" },(_chkDtr = new CheckBox{})),
                    new TableRow(new Label{Text="Enable Rts" },(_chkRts = new CheckBox{})),
                    new TableRow((_btnResetSerial = new Button { Text="Reset Serial" }), null),
                    new TableRow(),
                    null,
                },
            };
        }
    }
}
