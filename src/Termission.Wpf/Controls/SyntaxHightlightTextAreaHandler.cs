using Eto.Forms;
using Eto.Wpf.Forms;
using Eto.Wpf.Forms.Controls;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using Juniansoft.Termission.EtoForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Juniansoft.Termission.Wpf.Controls
{
    public class ExtendedTextEditor : ICSharpCode.AvalonEdit.TextEditor, INotifyPropertyChanged
    {
        public new string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(ExtendedTextEditor),
                new PropertyMetadata((obj, args) =>
                {
                    var target = (ExtendedTextEditor)obj;
                    target.Text = (string)args.NewValue;
                }));

        protected override void OnTextChanged(EventArgs e)
        {
            RaisePropertyChanged(nameof(Text));
            base.OnTextChanged(e);
        }

        public void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class SyntaxHightlightTextAreaHandler : WpfControl<ExtendedTextEditor, SyntaxHightlightTextArea, SyntaxHightlightTextArea.ICallback>, SyntaxHightlightTextArea.ISyntaxHightlightTextArea
    {
        protected int SuppressSelectionChanged { get; set; }
        protected int SuppressTextChanged { get; set; }
        //protected override sw.Size DefaultSize => new sw.Size(100, 60);

        protected override bool PreventUserResize { get { return true; } }

        public SyntaxHightlightTextAreaHandler()
        {

        }

        private ExtendedTextEditor editor;
        protected override ExtendedTextEditor CreateControl()
        {
            editor = new ExtendedTextEditor
            {
                ShowLineNumbers = true,
                FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#"),
            };
            UpdateLanguage();
            return editor;
        }

        public override void AttachEvent(string id)
        {
            switch (id)
            {
                case TextControl.TextChangedEvent:
                    Control.TextChanged += Control_TextChanged;
                    break;
                default:
                    base.AttachEvent(id);
                    break;
            }
        }

        void Control_TextChanged(object sender, EventArgs e)
        {
            if (SuppressTextChanged == 0)
                Callback.OnTextChanged(Widget, EventArgs.Empty);
        }

        public bool ReadOnly
        {
            get => Control.IsReadOnly;
            set => Control.IsReadOnly = value;
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

        public int CaretIndex
        {
            get => Control.CaretOffset;
            set => Control.CaretOffset = value;
        }

        public bool AcceptsTab { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AcceptsReturn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TextReplacements TextReplacements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TextReplacements SupportedTextReplacements => throw new NotImplementedException();

        public Eto.Forms.TextAlignment TextAlignment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool SpellCheck { get; set; }

        public bool SpellCheckIsSupported => false;

        public string Text
        {
            get => Control.Text;
            set
            {
                SuppressSelectionChanged++;
                Control.Text = value;
                SuppressSelectionChanged--;
                Control.Select(value?.Length ?? 0, 0);
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
                    UpdateLanguage();
                }
            }
        }

        internal void UpdateLanguage()
        {
            if (CodeLanguage == "js")
                editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("JavaScript");
            else if (CodeLanguage == "cs")
                editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
        }

        public void Append(string text, bool scrollToCursor)
        {
            Control.AppendText(text);
            if (scrollToCursor)
                Control.ScrollToEnd();
        }

        public void SelectAll()
        {
            Control.SelectAll();
        }
    }
}
