using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;

namespace Juniansoft.Termission.Core.Engines.Scripts
{
    public class CSharpCodeDomScriptEngine : BaseCSharpScriptEngine
    {
        public CSharpCodeDomScriptEngine()
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

            var codeProvider = new CSharpCodeProvider();
            var compilerParameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = false,
                TreatWarningsAsErrors = false,
                WarningLevel = 0,
            };
            compilerParameters.ReferencedAssemblies.AddRange(new[]
            {
                "System.dll",
                "System.Core.dll",
            });

            var compilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, sourceCode);

            Errors = new List<string> { };
            foreach (CompilerError err in compilerResults.Errors)
            {
                Errors.Add($"({err.Line},{err.Column}): error {err.ErrorNumber}: {err.ErrorText}");
            }

            if (Errors.Count == 0 && compilerResults.CompiledAssembly != null)
            {
                Type botFactoryType = compilerResults.CompiledAssembly.GetType(typeof(BotFactory).FullName);
                IsCompiled = CreateInstance(botFactoryType);
            }

            return IsCompiled;
        }
    }
}
