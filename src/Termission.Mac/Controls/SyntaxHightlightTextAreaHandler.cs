using System;
using AppKit.TextKit.Formatter;
using Eto.Forms;
using Eto.Mac.Forms.Controls;
using Juniansoft.Termission.EtoForms.Controls;

namespace Juniansoft.Termission.Mac.Controls
{
    public class SyntaxHightlightTextAreaHandler: TextAreaHandler<SyntaxHightlightTextArea, SyntaxHightlightTextArea.ICallback>, SyntaxHightlightTextArea.ISyntaxHightlightTextArea
    {
        private SourceTextView textSource;

        protected override AppKit.NSTextView CreateControl()
        {
            textSource = new SourceTextView(this)
            {
                SmartInsertDeleteEnabled = true,
                ContinuousSpellCheckingEnabled = false,
                CompleteClosures = true,
                WrapClosures = true
            };
            UpdateFormatter();
            return textSource;
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                textSource?.Formatter?.Reformat();
            }
        }

        private string _codeLanguage = "cs";
        public string CodeLanguage 
        {
            get => _codeLanguage;
            set
            {
                if (_codeLanguage != value)
                {
                    _codeLanguage = value;
                    UpdateFormatter();
                }
            }
        }

        private void UpdateFormatter()
        {
            if (CodeLanguage == "js")
            {
                textSource.Formatter = new LanguageFormatter(textSource, new JavaScriptDescriptor());
            }
            else if (CodeLanguage == "cs")
            {
                textSource.Formatter = new LanguageFormatter(textSource, new CSharpDescriptor());
            }
            textSource.Formatter.Reformat();
        }
    }
}
