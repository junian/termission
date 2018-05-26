[![GitHub release](https://img.shields.io/github/release/junian/termission.svg)](https://github.com/junian/termission/releases)
[![Github All Releases](https://img.shields.io/github/downloads/junian/termission/total.svg)](https://github.com/junian/termission/releases)

# Termission

Cross-platform Serial/TCP Terminal with Scriptable Auto-Response.

## About

This project is the successor of my old [serial-device-emulator](https://github.com/junian/serial-device-emulator). It's rewritten from the ground with cross-platform mindset.

## Features

- Cross-platform: available on Windows, macOS, and Linux
- Manual device control
- Can act as Serial COM device
- Can act as TCP Client/Server
- Log everything
- Auto response using JS/C# interpreter with .NET CLR

## Installation

- Install [VSPE](http://www.eterlogic.com/Products.VSPE.html) to create virtual Serial Port
- Download the app, install it, and use it

## Credits

This project is possible to build thanks to following libraries:

- [Eto.Forms](https://github.com/picoe/Eto) - A cross platform desktop and mobile user interface framework.
- [MvvmLightLibs](https://github.com/lbugnion/mvvmlight) - A toolkit to accelerate the creation and development of MVVM applications.
- [LitJson](https://github.com/LitJSON/litjson) - A .Net library to handle conversions from and to JSON (JavaScript Object Notation) strings.
- [Xam.Plugins.Settings](https://github.com/jamesmontemagno/SettingsPlugin) - Read and Write Settings Plugin for Xamarin and Windows.
- [FastColoredTextBox](https://github.com/PavelTorgashov/FastColoredTextBox) - A text editor component for .NET that allows you to create custom text editor with syntax highlighting.
- [SourceTextView](https://github.com/xamarin/mac-samples/tree/master/SourceWriter) - A simple source code editor for Xamarin.Mac that provides support for code completion and simple syntax highlighting.
- [AvalonEdit](https://github.com/icsharpcode/AvalonEdit) - The WPF-based text editor component used in SharpDevelop.
- [Mono.TextEditor](https://github.com/mono/monodevelop/tree/monodevelop-6.3.0.864/main/src/core/Mono.Texteditor) - GTK-based text editor developed by Monodevelop.
- [Jint](https://github.com/sebastienros/jint) - Javascript Interpreter for .NET Framework.
- [IronPython](https://github.com/IronLanguages/ironpython2) - Implementation of the Python programming language for .NET Framework.

## License

This project is licensed under [GPL-3.0](https://github.com/junian/termission/blob/master/LICENSE).

Copyright (C) 2018 Junian Triajianto
