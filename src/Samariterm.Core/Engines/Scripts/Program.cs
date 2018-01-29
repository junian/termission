namespace Juniansoft.Samariterm.Core.Engines.Scripts
{
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
