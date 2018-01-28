using System;
using System.Collections.Generic;

namespace Juniansoft.Samariterm.Core.Engines.Scripts
{
    public interface IBotScriptEngine
    {
        bool Compile(string code);
        IList<string> Errors { get; }
        byte[] GetResponse(byte[] data);
    }

    // Bot structure

    public interface IProgram
    {
        byte[] Main(byte[] args);
    }

    public class Program : IProgram
    {
        public byte[] Main(byte[] args) => throw new NotImplementedException();
    }

    public class BotFactory
    {
        public IProgram CreateProgram() => new Program();
    }
}
