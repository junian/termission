using System;
using System.Collections.Generic;
using System.Linq;
using Juniansoft.Termission.Core.Enums;
using Juniansoft.Termission.Core.Helpers;

namespace Juniansoft.Termission.Core.Models
{
    public class BotScript
    {
        public const string CSharpFileExtension = "csbot";
        public const string CSharpFileName = "C# Bot Script";
        public const string JavaScriptFileExtension = "jsbot";
        public const string JavaScriptFileName = "JS Bot Script";
        public const string PythonFileExtension = "pybot";
        public const string PythonFileName = "Python Bot Script";

        public BotScriptType ScriptType { get; private set; }
        public string FileExtension { get; private set; }
        public string FileTypeName { get; private set; }
        public string DefaultScript { get; private set; }

        private BotScript() { }

        private static IDictionary<BotScriptType, BotScript> _all = null;
        public static IDictionary<BotScriptType, BotScript> All
        {
            get
            {
                if (_all == null)
                {
                    _all = new List<BotScript>
                    {
                        new BotScript
                        {
                            ScriptType = BotScriptType.CSharp,
                            FileExtension = CSharpFileExtension,
                            FileTypeName = CSharpFileName,
                            DefaultScript = Settings.DefaultCSharpBotScript,
                        },
                        new BotScript
                        {
                            ScriptType = BotScriptType.JavaScript,
                            FileExtension = JavaScriptFileExtension,
                            FileTypeName = JavaScriptFileName,
                            DefaultScript = Settings.DefaultJSBotScript,
                        },
                        //new BotScript
                        //{
                        //    ScriptType = BotScriptType.Python,
                        //    FileExtension = PythonFileExtension,
                        //    FileTypeName = PythonFileName,
                        //    DefaultScript = Settings.DefaultPythonBotScript,
                        //},
                    }.ToDictionary(x => x.ScriptType);
                }
                return _all;
            }
        }

    }
}
