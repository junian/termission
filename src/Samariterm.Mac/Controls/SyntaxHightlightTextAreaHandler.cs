using System;
using AppKit.TextKit.Formatter;
using Eto.Forms;
using Eto.Mac.Forms.Controls;
using Juniansoft.Samariterm.EtoForms.Controls;
using Juniansoft.Samariterm.Mac.Controls;
using static Juniansoft.Samariterm.EtoForms.Controls.SyntaxHightlightTextArea;

namespace Juniansoft.Samariterm.Mac.Controls
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

        private Language _codeLanguage = Language.CSharp;
        public Language CodeLanguage 
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
            if (CodeLanguage == Language.JavaScript)
            {
                textSource.Formatter = new LanguageFormatter(textSource, new JavaScriptDescriptor());
            }
            else if (CodeLanguage == Language.CSharp)
            {
                textSource.Formatter = new LanguageFormatter(textSource, new CSharpDescriptor());
            }
            textSource.Formatter.Reformat();
        }
    }
}
