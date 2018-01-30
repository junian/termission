using System;
using System.Collections.Generic;

namespace Juniansoft.Samariterm.Core.Services
{
    public interface ISystemService
    {
        IList<string> GetAvailableNetworks();
        void Quit();
    }
}
