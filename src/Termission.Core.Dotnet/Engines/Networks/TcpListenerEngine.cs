using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Juniansoft.Termission.Core.Models;

namespace Juniansoft.Termission.Core.Engines.Networks
{
    public class TcpListenerEngine: BaseNetworkEngine
    {
        private TcpListener _tcpListener;
        private TcpClient _tcpClient;

        public TcpListenerEngine()
            : base()
        {
            _tcpListener = default(TcpListener);
            _tcpClient = default(TcpClient);
        }

        private Stream _baseStream;
        public override Stream BaseStream
        {
            get => _baseStream;
            protected set => _baseStream = value;
        }

        public override int ReadBufferSize
        {
            get => _tcpClient?.ReceiveBufferSize ?? 4096;
            set
            {
                if (_tcpClient != null)
                    _tcpClient.ReceiveBufferSize = value;
            }
        }

        public override bool IsOpen
        {
            get; protected set;
        }

        public override void Write(byte[] data, int offset, int count)
        {
            BaseStream?.Write(data, offset, count);
        }

        protected override void EngineClose()
        {
            IsOpen = false;
            BaseStream?.Dispose();
            BaseStream = null;
            _tcpClient?.Close();
            _tcpListener?.Stop();
        }

        protected override async Task EngineCloseAsync()
        {
            await Task.Run(() => EngineClose());
        }

        protected override void EngineOpen()
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Any, CurrentSettings.Port);
                _tcpListener?.Start();
                IsOpen = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                IsOpen = false;
            }
        }

        public override void Open()
        {
            EngineOpen();
            if (IsOpen)
            {
                _operation = AsyncOperationManager.CreateOperation(null);
                Task.Run(async () =>
                {
                    try
                    {
                        while (IsOpen)
                        {
                            _tcpClient = await _tcpListener.AcceptTcpClientAsync();
                            Console.WriteLine($"Connected with {_tcpClient}");
                            BaseStream = _tcpClient.GetStream();
                            while (await StreamReadAsync()) ;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                });
            }
        }

        protected override Task EngineOpenAsync()
        {
            throw new NotImplementedException();
        }

        private TcpListenerSettings CurrentSettings;

        public override void Set(object model)
        {
            if (model is TcpListenerSettings s)
            {
                CurrentSettings = s;
            }
        }
    }
}
