using System;
using Juniansoft.Termission.Core.Enums;

namespace Juniansoft.Termission.Core.Models
{
    public class TermSharpSettings
    {
        public TermSharpSettings()
        {
            SelectedBaudRate = 19200;
            SelectedDataBits = 8;
            SelectedHandshake = 0;
            SelectedParity = 0;
            SelectedStopBits = 0;

            SelectedActivityMode = ActivityMode.String;
            IsCR = IsLF = false;
        }

        public int SelectedBaudRate { get; set; }
        public int SelectedDataBits { get; set; }
        public int SelectedHandshake { get; set; }
        public int SelectedParity { get; set; }
        public int SelectedStopBits { get; set; }
        public ActivityMode SelectedActivityMode { get; set; }
        public bool IsLF { get; set; }
        public bool IsCR { get; set; }
    }
}
