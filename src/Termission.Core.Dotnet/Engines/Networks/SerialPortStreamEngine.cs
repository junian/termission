using System;
using System.IO;
using System.Threading.Tasks;
using Juniansoft.Termission.Core.Models;
using RJCP.IO.Ports;

namespace Juniansoft.Termission.Core.Engines.Networks
{
    public class SerialPortStreamEngine : BaseNetworkEngine
    {
        private SerialPortStream _serialPort;

        public SerialPortStreamEngine()
            : base()
        {
            _serialPort = new SerialPortStream();
            //_serialPort.ErrorReceived += (_, e) => OnLogReceived(e.EventType);
            //_serialPort.PinChanged += (_, e) => OnLogReceived(e.EventType);
        }

        public override Stream BaseStream
        {
            get => _serialPort;
            protected set
            {
                return;
            }
        }

        public override int ReadBufferSize
        {
            get => _serialPort?.ReadBufferSize ?? 4096;
            set
            {
                if (_serialPort != null)
                    _serialPort.ReadBufferSize = value;
            }
        }

        public override bool IsOpen
        {
            get => _serialPort?.IsOpen ?? false;
            protected set { return; }
        }

        public override void Set(object model)
        {
            if (model is SerialComSettings s)
            {
                _serialPort.BaudRate = s.BaudRate;
                _serialPort.Handshake = (Handshake)s.Handshake;
                _serialPort.Parity = (Parity)s.Parity;
                _serialPort.DataBits = s.DataBits;
                _serialPort.StopBits = (StopBits)s.StopBits;
            }
        }

        public override void Write(byte[] data, int offset, int count)
        {
            _serialPort?.Write(data, offset, count);
        }

        protected override void EngineClose()
        {
            _serialPort?.DiscardOutBuffer();
            _serialPort?.DiscardInBuffer();
            _serialPort?.Close();
        }

        protected override async Task EngineCloseAsync()
        {
            await Task.Run(() => EngineClose());
        }

        protected override void EngineOpen()
        {
            _serialPort?.Open();
        }

        protected override async Task EngineOpenAsync()
        {
            await Task.Run(() => EngineOpen());
        }
    }

}
