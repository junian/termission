using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mono.CSharp;

namespace Juniansoft.Termission.Core.Engines.Scripts
{
    public class CSharpMcsScriptEngine : BaseCSharpScriptEngine
    {
        private Evaluator _evaluator;

        public CSharpMcsScriptEngine()
            : base()
        {
        }

        /// <summary>
        /// compile script
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public override bool Compile(string code)
        {
            IsCompiled = false;

            var sourceCode = GetScript(code);

            var compilerSettings = new CompilerSettings
            {
                GenerateDebugInfo = false,
                WarningsAreErrors = false,
                WarningLevel = 0,
            };

            compilerSettings.AssemblyReferences.AddRange(new[]
            {
                "System.dll",
                "System.Core.dll",
            });

            var errSb = new StringBuilder();
            using (var writer = new StringWriter(errSb))
            {
                var streamReport = new StreamReportPrinter(writer);

                var ctx = new CompilerContext(compilerSettings, streamReport) { };

                _evaluator = new Evaluator(ctx);

                try
                {
                    var result = _evaluator.Compile(sourceCode);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                var errs = errSb.ToString();
                Errors = new List<string>();
                using (var reader = new StringReader(errs))
                {
                    var line = default(string);
                    while ((line = reader.ReadLine()) != null)
                    {
                        Errors.Add(line);
                    }
                }

                if (Errors.Count == 0)
                {
                    Type botFactoryType = (Type)_evaluator.Evaluate($"typeof({typeof(BotFactory).FullName});");
                    IsCompiled = CreateInstance(botFactoryType);
                }
            }

            return IsCompiled;
        }

    }
}
