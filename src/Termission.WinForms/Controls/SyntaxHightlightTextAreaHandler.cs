using Eto.Forms;
using Eto.WinForms.Forms;
using FastColoredTextBoxNS;
using Juniansoft.Termission.EtoForms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juniansoft.Termission.WinForms.Controls
{
    public class SyntaxHightlightTextAreaHandler : WindowsControl<FastColoredTextBox, SyntaxHightlightTextArea, SyntaxHightlightTextArea.ICallback>, SyntaxHightlightTextArea.ISyntaxHightlightTextArea
    {
        private FastColoredTextBox textBox;
        protected override FastColoredTextBox CreateControl()
        {
            textBox = new FastColoredTextBox();
            UpdateLanguage();
            return textBox;
        }

        public bool ReadOnly
        {
            get => Control.ReadOnly;
            set => Control.ReadOnly = value;
        }

        public bool Wrap
        {
            get => Control.WordWrap;
            set => Control.WordWrap = value;
        }

        public string SelectedText
        {
            get => Control.SelectedText;
            set => Control.SelectedText = value;
        }

        public Range<int> Selection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CaretIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool AcceptsTab
        {
            get => Control.AcceptsTab;
            set => Control.AcceptsTab = value;
        }

        public bool AcceptsReturn
        {
            get => Control.AcceptsReturn;
            set => Control.AcceptsReturn = value;
        }

        public TextReplacements TextReplacements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TextReplacements SupportedTextReplacements => throw new NotImplementedException();

        public TextAlignment TextAlignment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool SpellCheck { get; set; }

        public bool SpellCheckIsSupported => false;

        private string _codeLanguage = "cs";
        public string CodeLanguage
        {
            get => _codeLanguage;
            set
            {
                if (_codeLanguage != value)
                {
                    _codeLanguage = value;
                    UpdateLanguage();
                }
            }
        }

        internal void UpdateLanguage()
        {
            if (CodeLanguage == "js")
                textBox.Language = FastColoredTextBoxNS.Language.JS;
            else if (CodeLanguage == "cs")
                textBox.Language = FastColoredTextBoxNS.Language.CSharp;
            textBox.OnSyntaxHighlight(new TextChangedEventArgs(textBox.Range));
        }

        public void Append(string text, bool scrollToCursor)
        {
            Control.AppendText(text);
        }

        public void SelectAll()
        {
            Control.SelectAll();
        }
    }
}
