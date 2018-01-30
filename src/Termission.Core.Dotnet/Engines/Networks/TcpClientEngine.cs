using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Juniansoft.Termission.Core.Models;

namespace Juniansoft.Termission.Core.Engines.Networks
{
    public class TcpClientEngine: BaseNetworkEngine
    {
        TcpClient _tcpClient;

        public TcpClientEngine()
            : base()
        {
            _tcpClient = new TcpClient();
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
                {
                    _tcpClient.ReceiveBufferSize = value;
                }
            }
        }

        public override bool IsOpen
        {
            get
            {
                try
                {
                    if (_tcpClient != null && _tcpClient.Client != null && _tcpClient.Client.Connected)
                    {
                        /* pear to the documentation on Poll:
                         * When passing SelectMode.SelectRead as a parameter to the Poll method it will return 
                         * -either- true if Socket.Listen(Int32) has been called and a connection is pending;
                         * -or- true if data is available for reading; 
                         * -or- true if the connection has been closed, reset, or terminated; 
                         * otherwise, returns false
                         */

                        // Detect if client disconnected
                        if (_tcpClient.Client.Poll(0, SelectMode.SelectRead))
                        {
                            byte[] buff = new byte[1];
                            if (_tcpClient.Client.Receive(buff, SocketFlags.Peek) == 0)
                            {
                                // Client disconnected
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            protected set { return; }
        }

        public override void Write(byte[] data, int offset, int count)
        {
            BaseStream?.Write(data, offset, count);
        }

        protected override void EngineClose()
        {
            BaseStream?.Dispose();
            _tcpClient?.Close();
            BaseStream = null;
        }

        protected override async Task EngineCloseAsync()
        {
            await Task.Run(() => EngineClose());
        }

        protected override void EngineOpen()
        {
            _tcpClient = new TcpClient();
            _tcpClient?.Connect(IPAddress.Parse(CurrentSettings.IpAddress), CurrentSettings.Port);
            BaseStream = _tcpClient?.GetStream();
        }

        protected override async Task EngineOpenAsync()
        {
            _tcpClient = new TcpClient();
            await _tcpClient?.ConnectAsync(IPAddress.Parse(CurrentSettings.IpAddress), CurrentSettings.Port);
            BaseStream = _tcpClient?.GetStream();
        }

        private TcpClientSettings CurrentSettings;

        public override void Set(object model)
        {
            if (model is TcpClientSettings s)
            {
                CurrentSettings = s;
            }
        }
    }
}
