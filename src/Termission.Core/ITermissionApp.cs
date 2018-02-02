using System;
namespace Juniansoft.Termission.Core
{
    public interface ITermissionApp
    {
        void Run(string[] args);
        void RegisterServices();
    }
}
