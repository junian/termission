using System;
using System.IO;
using System.Threading.Tasks;
using Juniansoft.Termission.Core.Events;

namespace Juniansoft.Termission.Core.Engines.Networks
{
    public interface INetworkEngine
    {
        event EventHandler<MessageResponseReceivedArgs> MessageResponseReceived;

        Stream BaseStream { get; }

        int ReadBufferSize { get; set; }

        void Set(object model);

        void Write(byte[] data);
        void Write(byte[] data, int offset, int count);
        void Open();
        Task OpenAsync();
        void Close();
        Task CloseAsync();
        bool IsOpen { get; }
    }
}
