using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Juniansoft.Termission.Core.Engines.Scripts
{
    public abstract class BaseCSharpScriptEngine : BaseScriptEngine
    {
        protected object _botInstance;

        public bool IsCompiled { get; protected set; }
        public int LineNumber { get; protected set; }

        protected BaseCSharpScriptEngine()
            : base()
        {
            IsCompiled = false;
        }

        protected string GetScript(string script)
        {
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            var resourceName = $"{typeof(Program).FullName}.cs";
            var sb = new StringBuilder();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var lineNumber = 0;
                var found = false;
                var line = default(string);

                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        if (!found)
                        {
                            if (line.Contains("public class Program"))
                            {
                                found = true;
                                sb.AppendLine(script);
                            }
                            else
                            {
                                lineNumber++;
                                sb.AppendLine(line);
                            }
                        }
                        else
                        {
                            sb.AppendLine(line);
                        }
                    }
                }

                LineNumber = lineNumber;
            }

            return sb.ToString();
        }

        protected bool CreateInstance(Type type)
        {
            try
            {
                var botFactory = Activator.CreateInstance(type);
                MethodInfo method = botFactory.GetType().GetMethod(nameof(BotFactory.CreateProgram));
                _botInstance = method.Invoke(botFactory, null);
            }
            catch (Exception ex)
            {
                ex.GetType();
                return false;
            }
            return true;
        }

        public override byte[] GetResponse(byte[] data)
        {
            byte[] response = null;
            object[] parameters = new object[1] { data };
            if (IsCompiled && _botInstance != null)
            {
                MethodInfo method = _botInstance.GetType().GetMethod(nameof(IProgram.Main));
                response = (byte[])method.Invoke(_botInstance, parameters);
            }
            return response;
        }
    }
}
