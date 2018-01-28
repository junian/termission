using System;
using System.IO;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Juniansoft.Samariterm.Core.Engines.Networks
{
    public class SerialComEngine: BaseNetworkEngine
    {
        private SerialPort _serialPort;

        public SerialComEngine()
            : base()
        {
            _serialPort = new SerialPort();
            //_serialPort.ErrorReceived += (_, e) => OnLogReceived(e.EventType);
            //_serialPort.PinChanged += (_, e) => OnLogReceived(e.EventType);
        }

        public override Stream BaseStream
        {
            get => _serialPort.BaseStream;
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
            if (model is Settings s)
            {
                _serialPort.BaudRate = s.BaudRate;
                _serialPort.Handshake = s.Handshake;
                _serialPort.Parity = s.Parity;
                _serialPort.DataBits = s.DataBits;
                _serialPort.StopBits = s.StopBits;
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

        public class Settings
        {
            public int BaudRate { get; set; }
            public Handshake Handshake { get; set; }
            public Parity Parity { get; set; }
            public int DataBits { get; set; }
            public StopBits StopBits { get; set; }
        }
    }

}
