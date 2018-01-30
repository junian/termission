using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juniansoft.Termission.Core.Events;
using Juniansoft.Termission.Core.Models;

namespace Juniansoft.Termission.Core.Engines.Networks
{
    public class TermSharpEngine: INetworkEngine
    {
        SerialComEngine _serialComEngine;
        TcpClientEngine _tcpClientEngine;
        TcpListenerEngine _tcpListenerEngine;

        INetworkEngine _selectedEngine;

        public TermSharpEngine()
        {
            _serialComEngine = new SerialComEngine();
            _serialComEngine.MessageResponseReceived += (_, e) => OnMessageResponseReceived(e.Data);

            _tcpClientEngine = new TcpClientEngine();
            _tcpClientEngine.MessageResponseReceived += (_, e) => OnMessageResponseReceived(e.Data);

            _tcpListenerEngine = new TcpListenerEngine();
            _tcpListenerEngine.MessageResponseReceived += (_, e) => OnMessageResponseReceived(e.Data);

            _selectedEngine = _serialComEngine;
        }

        public Stream BaseStream => _selectedEngine?.BaseStream;

        public int ReadBufferSize
        {
            get => _selectedEngine?.ReadBufferSize ?? 4096;
            set
            {
                if (_selectedEngine != null)
                {
                    _selectedEngine.ReadBufferSize = value;
                }
            }
        }

        public void Set(object model)
        {
            if (model is SerialComSettings)
                _selectedEngine = _serialComEngine;
            else if (model is TcpClientSettings)
                _selectedEngine = _tcpClientEngine;
            else if (model is TcpListenerSettings)
                _selectedEngine = _tcpListenerEngine;

            _selectedEngine?.Set(model);
        }

        public bool IsOpen => _selectedEngine?.IsOpen ?? false;

        protected void OnMessageResponseReceived(byte[] response)
        {
            MessageResponseReceived?.Invoke(this, new MessageResponseReceivedArgs(response));
        }

        public event EventHandler<MessageResponseReceivedArgs> MessageResponseReceived;

        public void Close()
        {
            _selectedEngine.Close();
        }

        public async Task CloseAsync()
        {
            await _selectedEngine.CloseAsync();
        }

        public void Open()
        {
            _selectedEngine.Open();
        }

        public async Task OpenAsync()
        {
            await _selectedEngine.OpenAsync();
        }

        public void Write(byte[] data)
        {
            _selectedEngine.Write(data);
        }

        public void Write(byte[] data, int offset, int count)
        {
            _selectedEngine.Write(data, offset, count);
        }
    }
}
