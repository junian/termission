ace.require("ace/ext/language_tools");
var editor = ace.edit("editor");
//editor.setTheme("ace/theme/twilight");
editor.session.setMode("ace/mode/csharp");
editor.setOptions({
    enableBasicAutocompletion: true
});
