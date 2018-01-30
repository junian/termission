using Eto;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juniansoft.Termission.EtoForms.Controls
{
    [Handler(typeof(ISyntaxHightlightTextArea))]
    public class SyntaxHightlightTextArea : TextArea
    {
        public enum Language
        {
            CSharp,
            JavaScript
        }

        new ISyntaxHightlightTextArea Handler { get { return (ISyntaxHightlightTextArea)base.Handler; } }

        public string CodeLanguage
        {
            get { return Handler.CodeLanguage; }
            set { Handler.CodeLanguage = value; }
        }

        // interface to the platform implementations
        public interface ISyntaxHightlightTextArea : IHandler
        {
            string CodeLanguage { get; set; }
        }
    }
}
