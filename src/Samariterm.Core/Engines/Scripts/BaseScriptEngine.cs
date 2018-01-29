﻿using System;
using System.Collections.Generic;

namespace Juniansoft.Samariterm.Core.Engines.Scripts
{
    public abstract class BaseScriptEngine: IBotScriptEngine
    {
        protected BaseScriptEngine()
        {
            Errors = new List<string>();
        }

        public IList<string> Errors { get; protected set; }

        public abstract bool Compile(string code);

        public abstract byte[] GetResponse(byte[] data);
    }
}
