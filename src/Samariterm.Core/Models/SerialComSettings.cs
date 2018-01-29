using System;
namespace Juniansoft.Samariterm.Core.Models
{
    public class SerialComSettings
    {
        public int BaudRate { get; set; }
        public int Handshake { get; set; }
        public int Parity { get; set; }
        public int DataBits { get; set; }
        public int StopBits { get; set; }
    }
}
