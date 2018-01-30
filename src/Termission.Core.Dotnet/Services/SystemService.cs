using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using Juniansoft.Termission.Core.Helpers;

namespace Juniansoft.Termission.Core.Services
{
    public class SystemService: ISystemService
    {
        public SystemService()
        {
        }

        public IList<string> GetAvailableNetworks()
        {
            int p = (int)System.Environment.OSVersion.Platform;
            var result = new List<string>
            {
                $"TCP Listener [{Settings.TcpListenerIpAddress}:{Settings.TcpListenerPort}]",
                $"TCP Client [{Settings.TcpClientIpAddress}:{Settings.TcpClientPort}]"
            };

            // Are we on Unix?
            if (p == 4 || p == 128 || p == 6)
            {
#if DEBUG
                Console.WriteLine("It's debugging.");
                var homePath = Environment.GetEnvironmentVariable("HOME");
                var dbgPath = Path.Combine(homePath, "dev");
                Console.WriteLine($"{nameof(dbgPath)}: {dbgPath}");
                string[] dbg = System.IO.Directory.GetFiles(dbgPath, "tty.*", SearchOption.TopDirectoryOnly);
                result.AddRange(dbg.Where(x => x.StartsWith($"{dbgPath}/tty.", StringComparison.Ordinal)));
#endif
                string[] ttys = System.IO.Directory.GetFiles("/dev/", "tty.*", SearchOption.TopDirectoryOnly);
                result.AddRange(ttys.Where(x => x.StartsWith("/dev/tty.", StringComparison.Ordinal)));
                string[] cus = System.IO.Directory.GetFiles("/dev/", "cu.*", SearchOption.TopDirectoryOnly);
                result.AddRange(cus.Where(x => x.StartsWith("/dev/cu.", StringComparison.Ordinal)));
            }
            else
            {
                var portNames = SerialPort.GetPortNames();
                Array.Sort(portNames);
                if (portNames != null)
                {
                    result.AddRange(portNames);
                }
            }

            return result;
        }

        public virtual void Quit() { }
    }
}
