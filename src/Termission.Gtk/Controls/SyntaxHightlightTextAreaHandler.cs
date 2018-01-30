using System;
using Eto.Drawing;
using Eto.Forms;
using Eto.GtkSharp.Forms;
using Juniansoft.Termission.EtoForms.Controls;
using Mono.TextEditor;
using Mono.TextEditor.Highlighting;

namespace Juniansoft.Termission.GtkSharp.Controls
{
    public class SyntaxHightlightTextAreaHandler : GtkControl<MonoTextEditor, SyntaxHightlightTextArea, SyntaxHightlightTextArea.ICallback>, SyntaxHightlightTextArea.ISyntaxHightlightTextArea
    {
        MonoTextEditor editor;
        public SyntaxHightlightTextAreaHandler()
        {

            scroll = new Gtk.ScrolledWindow
            {
                ShadowType = Gtk.ShadowType.In,
            };
            editor = new MonoTextEditor
            {
                Options = new TextEditorOptions
                {
                    HighlightMatchingBracket = true,
                    FontName = "Monaco 10",
                    ColorScheme = "Visual Studio",
                    ShowWhitespaces = ShowWhitespaces.Selection,
                    TabsToSpaces = true,
                    ShowLineNumberMargin = true,
                    EnableSyntaxHighlighting = true,
                    EnableAnimations = true,
                },
            };

            //data.ModifyFont(Pango.FontDescription.FromString($"Courier 12"));
            UpdateLanguage();
            Control = editor;
            Size = new Size(100, 60);
            scroll.Add(Control);
            Wrap = true;
        }

        int suppressSelectionAndTextChanged;
        readonly Gtk.ScrolledWindow scroll;
        Gtk.TextTag tag;

        public override Gtk.Widget ContainerControl
        {
            get { return scroll; }
        }

        public override Size DefaultSize { get { return new Size(100, 60); } }

        public override void AttachEvent(string id)
        {
            switch (id)
            {
                case TextControl.TextChangedEvent:
                    Control.Document.TextSet += Connector.HandleBufferChanged;
                    break;
                //case TextArea.SelectionChangedEvent:
                //    Control.Document.TextSet += Connector.HandleSelectionChanged;
                //    break;
                //case TextArea.CaretIndexChangedEvent:
                //    Control.Buffer.MarkSet += Connector.HandleCaretIndexChanged;
                //    break;
                default:
                    base.AttachEvent(id);
                    break;
            }
        }

        protected new MonoTextEditorConnector Connector { get { return (MonoTextEditorConnector)base.Connector; } }

        protected override WeakConnector CreateConnector()
        {
            return new MonoTextEditorConnector();
        }

        public override string Text
        {
            get { return Control.Document.Text; }
            set
            {
                var sel = Selection;
                suppressSelectionAndTextChanged++;
                Control.Document.Text = value;
                //if (tag != null)
                //    Control.Buffer.ApplyTag(tag, Control.Buffer.StartIter, Control.Buffer.EndIter);
                Callback.OnTextChanged(Widget, EventArgs.Empty);
                suppressSelectionAndTextChanged--;
                if (sel != Selection)
                    Callback.OnSelectionChanged(Widget, EventArgs.Empty);
            }
        }

        public virtual Color TextColor
        {
            get; set;
        }

        public override Color BackgroundColor
        {
            get; set;
        }

        public bool ReadOnly
        {
            get { return Control.Document.ReadOnly; }
            set { Control.Document.ReadOnly = value; }
        }

        public bool Wrap
        {
            get; set;
        }

        public void Append(string text, bool scrollToCursor)
        {
            Control.Text += text;
            //var end = Control.Buffer.EndIter;
            //Control.Buffer.Insert(ref end, text);
            //if (scrollToCursor)
            //{
            //    var mark = Control.Buffer.CreateMark(null, end, false);
            //    Control.ScrollToMark(mark, 0, false, 0, 0);
            //}
        }

        public string SelectedText
        {
            get; set;
        }

        public Range<int> Selection
        {
            get; set;
        }

        public void SelectAll()
        {
            //Control.SetSelection()
        }

        public int CaretIndex
        {
            get; set;
            //get { return Control.Buffer.GetIterAtMark(Control.Buffer.InsertMark).Offset; }
            //set
            //{
            //    var ins = Control.Buffer.GetIterAtOffset(value);
            //    Control.Buffer.SelectRange(ins, ins);
            //}
        }

        public bool AcceptsTab
        {
            get; set;
        }

        bool acceptsReturn = true;

        public bool AcceptsReturn
        {
            get; set;
        }

        void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                e.Handled = true;
        }

        static void PreventEnterKey(object o, Gtk.KeyPressEventArgs args)
        {
            if (args.Event.Key == Gdk.Key.Return)
                args.RetVal = false;
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
            }
        }

        public TextAlignment TextAlignment
        {
            get; set;
        }

        public bool SpellCheck
        {
            get { return false; }
            set { }
        }

        public bool SpellCheckIsSupported { get { return false; } }

        public TextReplacements TextReplacements
        {
            get { return TextReplacements.None; }
            set { }
        }

        public TextReplacements SupportedTextReplacements
        {
            get { return TextReplacements.None; }
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
            var mime = "text/x-csharp";
            if (CodeLanguage == "js")
                mime = "text/javascript";
            editor.Document.SyntaxMode = SyntaxModeService.GetSyntaxMode(editor.Document, mime);
        }

        protected class MonoTextEditorConnector : GtkControlConnector
        {
            Range<int> lastSelection;
            int? lastCaretIndex;

            public new SyntaxHightlightTextAreaHandler Handler { get { return (SyntaxHightlightTextAreaHandler)base.Handler; } }

            public void HandleBufferChanged(object sender, EventArgs e)
            {
                var handler = Handler;
                if (handler.suppressSelectionAndTextChanged == 0)
                    handler.Callback.OnTextChanged(Handler.Widget, EventArgs.Empty);
            }

            public void HandleSelectionChanged(object o, Gtk.MarkSetArgs args)
            {
                var handler = Handler;
                var selection = handler.Selection;
                if (handler.suppressSelectionAndTextChanged == 0 && selection != lastSelection)
                {
                    handler.Callback.OnSelectionChanged(handler.Widget, EventArgs.Empty);
                    lastSelection = selection;
                }
            }

            public void HandleCaretIndexChanged(object o, Gtk.MarkSetArgs args)
            {
                var handler = Handler;
                var caretIndex = handler.CaretIndex;
                if (handler.suppressSelectionAndTextChanged == 0 && caretIndex != lastCaretIndex)
                {
                    handler.Callback.OnCaretIndexChanged(handler.Widget, EventArgs.Empty);
                    lastCaretIndex = caretIndex;
                }
            }

            public void HandleApplyTag(object sender, EventArgs e)
            {
                //var buffer = Handler.Control.Document.;
                //var tag = Handler.tag;
                //buffer.ApplyTag(tag, buffer.StartIter, buffer.EndIter);
            }
        }
    }

}
