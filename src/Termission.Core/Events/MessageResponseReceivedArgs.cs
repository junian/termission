using System;
namespace Juniansoft.Termission.Core.Events
{
    public class MessageResponseReceivedArgs: EventArgs
    {
        public byte[] Data { get; private set; }

        public MessageResponseReceivedArgs(byte[] data)
        {
            Data = data;
        }
    }
}
