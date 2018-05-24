using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Juniansoft.MvvmReady;
using Juniansoft.Termission.Core.Engines.Scripts;
using Juniansoft.Termission.Core.Enums;
using Juniansoft.Termission.Core.Helpers;
using Juniansoft.Termission.Core.Models;
using Juniansoft.Termission.Core.Services;

namespace Juniansoft.Termission.Core.ViewModels
{
    public class DeviceBotViewModel: CoreViewModel
    {
        private BotScript _selectedBotScript;

        private ICrossDialog _crossFileDialog;
        private IFileService _file;

        private IBotScriptEngine _deviceBotEngine;
        public IBotScriptEngine DeviceBotEngine
        {
            get => _deviceBotEngine;
            set => _deviceBotEngine = value;
        }

        private bool _isCompiled = false;
        public bool IsCompiled
        {
            get => _isCompiled;
            set => Set(ref _isCompiled, value);
        }

        private bool _isCompileStatusVisible = false;
        public bool IsCompileStatusVisible
        {
            get => _isCompileStatusVisible;
            set => Set(ref _isCompileStatusVisible, value);
        }

        public bool IsBotEnabled
        {
            get => Settings.DeviceBotEnabled;
            set
            {
                if (Settings.DeviceBotEnabled != value)
                {
                    Settings.DeviceBotEnabled = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(BotStatus));
                }
            }
        }

        public string BotStatus
        {
            get => IsBotEnabled ? "Enabled" : "Disabled";
        }

        public string UserScript
        {
            get => Settings.CurrentBotScript;
            set
            {
                if (Settings.CurrentBotScript != value)
                {
                    Settings.CurrentBotScript = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand CompileCommand => new Command(() =>
        {
            CompileFinishedAction?.Invoke(Compile());
        });

        public Action<string> CompileFinishedAction { get; set; }

        public string Compile()
        {
            try
            {
                if (DeviceBotEngine.Compile(UserScript))
                {
                    IsCompiled = true;
                    IsCompileStatusVisible = true;
                }
                else
                {
                    var sb = new StringBuilder();
                    foreach (var ce in DeviceBotEngine.Errors)
                    {
                        sb.AppendLine($"{ce}");
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return string.Empty;
        }

        public virtual ICommand NewCommand => new Command(() =>
        {
            UserScript = _selectedBotScript.DefaultScript;
            CurrentFilePath = string.Empty;
        });

        public virtual ICommand OpenCommand => new Command(async () =>
        {
            var filename = await _crossFileDialog.ShowOpenDialogAsync(FileExtenstion, FileTypeName);

            var content = LoadFromFile(filename);
            if (content != null)
            {
                UserScript = content;
            }
        });

        public virtual ICommand SaveCommand => new Command(async () =>
        {
            if (!_file.Exists(CurrentFilePath))
            {
                SaveAsCommand?.Execute(null);
                return;
            }

            var content = UserScript;
            await Task.Run(() => SaveToFile(content, CurrentFilePath));
        });

        public virtual ICommand SaveAsCommand => new Command(async () =>
        {
            var content = UserScript;
            var filename = await _crossFileDialog.ShowSaveDialogAsync(FileExtenstion, FileTypeName);
            SaveToFile(content, filename);
        });

        public string FileExtenstion => _selectedBotScript.FileExtension;

        public string FileTypeName => _selectedBotScript.FileTypeName;

        protected virtual string LoadFromFile(string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename) && _file.Exists(filename))
            {
                CurrentFilePath = filename;
                var content = _file.ReadAllText(CurrentFilePath);
                return content;
            }

            return null;
        }

        protected virtual void SaveToFile(string content, string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                CurrentFilePath = filename;
                _file.WriteAllText(CurrentFilePath, content);
            }
        }

        public string CurrentFilePath
        {
            get => Settings.CurrentBotFile;
            set
            {
                if (Settings.CurrentBotFile != value)
                {
                    Settings.CurrentBotFile = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public string Title
        {
            get => string.IsNullOrWhiteSpace(CurrentFilePath) ? $"{CoreApp.AssemblyProduct} Bot" : $"{CoreApp.AssemblyProduct} Bot ({CurrentFilePath})";
        }

        public int Width
        {
            get => Settings.DeviceBotWidth;
            set
            {
                if (Settings.DeviceBotWidth != value)
                {
                    Settings.DeviceBotWidth = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int Height
        {
            get => Settings.DeviceBotHeight;
            set
            {
                if (Settings.DeviceBotHeight != value)
                {
                    Settings.DeviceBotHeight = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DeviceBotViewModel()
        {
            _crossFileDialog = ServiceLocator.Current.Get<ICrossDialog>();
            _file = ServiceLocator.Current.Get<IFileService>();
            UpdateBotLang(SelectedBotLanguage);
        }

        public ObservableCollection<BotScriptType> BotLanguages => new ObservableCollection<BotScriptType>(BotScript.All.Keys);
        public BotScriptType SelectedBotLanguage
        {
            get => (BotScriptType)Settings.SelectedScriptingLanguage;
            set
            {
                var botLang = (int)value;
                if (Settings.SelectedScriptingLanguage != botLang)
                {
                    Settings.SelectedScriptingLanguage = botLang;
                    UpdateBotLang(value);
                    RaisePropertyChanged();
                }
            }
        }

        private string _codeLanguage;
        public string CodeLanguage
        {
            get => _codeLanguage;
            set => Set(ref _codeLanguage, value);
        }

        private void UpdateBotLang(BotScriptType type)
        {
            _selectedBotScript = BotScript.All[type];
            if (type == BotScriptType.JavaScript)
            {
                DeviceBotEngine = ServiceLocator.Current.Get<IJavaScriptBotEngine>();
                CodeLanguage = "js";
            }
            else
            {
                DeviceBotEngine = ServiceLocator.Current.Get<ICSharpBotEngine>();
                CodeLanguage = "cs";
            }
            IsCompiled = false;
        }
    }
}
