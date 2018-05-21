// Helpers/Settings.cs
using Juniansoft.Termission.Core.Enums;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Juniansoft.Termission.Core.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public const string DefaultTcpListenerIpAddress = "127.0.0.1";
        public static string TcpListenerIpAddress
        {
            get => AppSettings.GetValueOrDefault(nameof(TcpListenerIpAddress), DefaultTcpListenerIpAddress);
            set => AppSettings.AddOrUpdateValue(nameof(TcpListenerIpAddress), value);
        }

        public const int DefaultTcpListenerPort = 11500;
        public static int TcpListenerPort
        {
            get => AppSettings.GetValueOrDefault(nameof(TcpListenerPort), DefaultTcpListenerPort);
            set => AppSettings.AddOrUpdateValue(nameof(TcpListenerPort), value);
        }

        public const string DefaultTcpClientIpAddress = "127.0.0.1";
        public static string TcpClientIpAddress
        {
            get => AppSettings.GetValueOrDefault(nameof(TcpClientIpAddress), DefaultTcpClientIpAddress);
            set => AppSettings.AddOrUpdateValue(nameof(TcpClientIpAddress), value);
        }

        public const int DefaultTcpClientPort = 11500;
        public static int TcpClientPort
        {
            get => AppSettings.GetValueOrDefault(nameof(TcpClientPort), DefaultTcpClientPort);
            set => AppSettings.AddOrUpdateValue(nameof(TcpClientPort), value);
        }

        public static bool DeviceBotEnabled
        {
            get => AppSettings.GetValueOrDefault(nameof(DeviceBotEnabled), false);
            set => AppSettings.AddOrUpdateValue(nameof(DeviceBotEnabled), value);
        }

        public static bool IsCR
        {
            get => AppSettings.GetValueOrDefault(nameof(IsCR), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsCR), value);
        }

        public static bool IsLF
        {
            get => AppSettings.GetValueOrDefault(nameof(IsLF), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsLF), value);
        }

        public static string SendMessage
        {
            get => AppSettings.GetValueOrDefault(nameof(SendMessage), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(SendMessage), value);
        }

        public static bool MainFormTopMost
        {
            get => AppSettings.GetValueOrDefault(nameof(MainFormTopMost), false);
            set => AppSettings.AddOrUpdateValue(nameof(MainFormTopMost), value);
        }

        public static int SelectedActivityMode
        {
            get => AppSettings.GetValueOrDefault(nameof(SelectedActivityMode), (int)ActivityMode.String);
            set => AppSettings.AddOrUpdateValue(nameof(SelectedActivityMode), value);
        }

        public const int DefaultBaudRate = 9600;
        public static int SelectedBaudRate
        {
            get => AppSettings.GetValueOrDefault(nameof(SelectedBaudRate), DefaultBaudRate);
            set => AppSettings.AddOrUpdateValue(nameof(SelectedBaudRate), value);
        }

        public const int DefaultDataBits = 8;
        public static int SelectedDataBits
        {
            get => AppSettings.GetValueOrDefault(nameof(SelectedDataBits), DefaultDataBits);
            set => AppSettings.AddOrUpdateValue(nameof(SelectedDataBits), value);
        }

        public const int DefaultStopBits = 1;
        public static int SelectedStopBits
        {
            get => AppSettings.GetValueOrDefault(nameof(SelectedStopBits), DefaultStopBits);
            set => AppSettings.AddOrUpdateValue(nameof(SelectedStopBits), value);
        }

        public const int DefaultHandshake = 0;
        public static int SelectedHandshake
        {
            get => AppSettings.GetValueOrDefault(nameof(SelectedHandshake), DefaultHandshake);
            set => AppSettings.AddOrUpdateValue(nameof(SelectedHandshake), value);
        }

        public const int DefaultParity = 0;
        public static int SelectedParity
        {
            get => AppSettings.GetValueOrDefault(nameof(SelectedParity), DefaultParity);
            set => AppSettings.AddOrUpdateValue(nameof(SelectedParity), value);
        }

        public const bool DefaultDtrEnable = false;
        public static bool IsDtrEnable
        {
            get => AppSettings.GetValueOrDefault(nameof(IsDtrEnable), DefaultDtrEnable);
            set => AppSettings.AddOrUpdateValue(nameof(IsDtrEnable), value);
        }

        public const bool DefaultRtsEnable = false;
        public static bool IsRtsEnable
        {
            get => AppSettings.GetValueOrDefault(nameof(IsRtsEnable), DefaultRtsEnable);
            set => AppSettings.AddOrUpdateValue(nameof(IsRtsEnable), value);
        }

        public static int SelectedScriptingLanguage
        {
            get => AppSettings.GetValueOrDefault(nameof(SelectedScriptingLanguage), (int)BotScriptType.CSharp);
            set => AppSettings.AddOrUpdateValue(nameof(SelectedScriptingLanguage), value);
        }

        public const string DefaultCSharpBotScript =
@"// Return value must be array of bytes
// Return null if it means no response.
// Use `byte[] args` to see received message.
// Use Encoding.ASCII.GetBytes(string) to return a string response.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Do not remove this `Program` class and its `Main` function. 
// It is mandatory for your script to work properly.
// Other than that, feel free to create any classes and functions.
public class Program: IProgram
{
    public byte[] Main(byte[] args)
    {
        // Put your response code here
        
        return null;
    }
}
";

        public const string DefaultJSBotScript =
@"
function main(args)
{
    return null;
}
";

        public const string DefaultPythonBotScript =
@"

";

        public static string CurrentBotScript
        {
            get => AppSettings.GetValueOrDefault(nameof(CurrentBotScript), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(CurrentBotScript), value);
        }

        public static string CurrentSettingsFile
        {
            get => AppSettings.GetValueOrDefault(nameof(CurrentSettingsFile), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(CurrentSettingsFile), value);
        }

        public static string CurrentBotFile
        {
            get => AppSettings.GetValueOrDefault(nameof(CurrentBotFile), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(CurrentBotFile), value);
        }

        public static int DeviceBotWidth
        {
            get => AppSettings.GetValueOrDefault(nameof(DeviceBotWidth), 640);
            set => AppSettings.AddOrUpdateValue(nameof(DeviceBotWidth), value);
        }

        public static int DeviceBotHeight
        {
            get => AppSettings.GetValueOrDefault(nameof(DeviceBotHeight), 480);
            set => AppSettings.AddOrUpdateValue(nameof(DeviceBotHeight), value);
        }
    }
}
