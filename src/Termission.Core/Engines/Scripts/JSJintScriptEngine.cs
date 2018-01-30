using System;
using System.Collections.Generic;
using System.Linq;
using Jint;
using Jint.Parser;
using Jint.Runtime;

namespace Juniansoft.Termission.Core.Engines.Scripts
{
    public class JSJintScriptEngine: BaseJSScriptEngine
    {
        private Engine _jint;
        private string _botInstance;

        public JSJintScriptEngine()
            : base()
        {
            _jint = new Engine(cfg => cfg.AllowClr());
        }

        public override bool Compile(string code)
        {
            try
            {
                _jint = new Engine(cfg => cfg.AllowClr());
                _jint = _jint.Execute(code);
                var isMainExists = (bool)_jint.Execute("typeof main === \"function\"").GetCompletionValue().ToObject();

                Errors = new List<string>();

                if (!isMainExists)
                {
                    Errors = new List<string>
                    {
                        "`main(args)` function doesn't exist.",
                    };
                    return false;
                }

                _botInstance = code;
                return true;
            }
            catch (ParserException ex)
            {
                Errors = new List<string>
                {
                    ex.Message,
                };
            }
            catch (JavaScriptException ex)
            {
                Errors = new List<string>
                {
                    ex.Message,
                };
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public override byte[] GetResponse(byte[] data)
        {
            var result = (object[])_jint
                .Invoke("main", data)
                .ToObject();

            if (result == null) return null;

            return result.Select(x => Convert.ToByte(x)).ToArray();
        }


    }
}
