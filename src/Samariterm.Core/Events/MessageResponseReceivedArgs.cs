using System;
namespace Juniansoft.Samariterm.Core.Events
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
