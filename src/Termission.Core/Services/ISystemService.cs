using System;
using System.Collections.Generic;

namespace Juniansoft.Termission.Core.Services
{
    public interface ISystemService
    {
        IList<string> GetAvailableNetworks();
        void Quit();
    }
}
