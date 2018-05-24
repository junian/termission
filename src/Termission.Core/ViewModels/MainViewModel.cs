using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Juniansoft.MvvmReady;
using Juniansoft.Termission.Core.Engines;
using Juniansoft.Termission.Core.Engines.Networks;
using Juniansoft.Termission.Core.Engines.Scripts;
using Juniansoft.Termission.Core.Enums;
using Juniansoft.Termission.Core.Helpers;
using Juniansoft.Termission.Core.Models;
using Juniansoft.Termission.Core.Services;

namespace Juniansoft.Termission.Core.ViewModels
{
    public class MainViewModel: CoreViewModel
    {

        const string RunString = "&Run";
        const string StopString = "&Stop";

        private Encoding _enc;

        private INetworkEngine _engine;

        public IBotScriptEngine DeviceBotEngine { get; set; }

        private INotificationService _notification;
        private ICrossDialog _crossFileDialog;
        private ISystemService _systemService;
        private IFileService _file;
        private IJsonService _jsonSvc;

        public ObservableCollection<int> BaudRateOptions => new ObservableCollection<int>
        {
            600,
            1200,
            2400,
            4800,
            9600,
            14400,
            19200,
            28800,
            38400,
            56000,
            57600,
            115200,
            128000,
            256000,
        };

        //private int _selectedBaudRate = 19200;
        public int SelectedBaudRate
        {
            get => Settings.SelectedBaudRate;
            set
            {
                if (Settings.SelectedBaudRate != value)
                {
                    Settings.SelectedBaudRate = value;
                    RaisePropertyChanged();
                }
            }
        }

        IList<string> _serialPortList;
        public IList<string> SerialPortList
        {
            get { return _serialPortList; }
            set { this.Set(ref _serialPortList, value); }
        }

        private string _selectedSerialPortName;
        public string SelectedSerialPortName
        {
            get => _selectedSerialPortName;
            set => Set(ref _selectedSerialPortName, value);
        }

        //private int _selectedSerialPortIndex = -1;
        //public int SelectedSerialPortIndex
        //{
        //    get => Math.Max(-1, Math.Min(_selectedSerialPortIndex, SerialPortList.Count - 1));
        //    set => Set(ref _selectedSerialPortIndex, Math.Max(-1, Math.Min(value, SerialPortList.Count - 1)));
        //}

        public ObservableCollection<int> DataBitsOptions => new ObservableCollection<int>
        {
            5,
            6,
            7,
            8,
        };

        //private int _selectedDataBits = 8;
        public int SelectedDataBits
        {
            get => Settings.SelectedDataBits;
            set
            {
                if (Settings.SelectedDataBits != value)
                {
                    Settings.SelectedDataBits = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<string> StopBitsOptions => new ObservableCollection<string>
        {
            "1",
            "1.5",
            "2",
        };

        //private StopBits _selectedStopBits = StopBits.One;
        public int SelectedStopBits
        {
            get => (int)Settings.SelectedStopBits;
            set
            {
                var stopbits = (int)value;
                if (Settings.SelectedStopBits != stopbits)
                {
                    Settings.SelectedStopBits = stopbits;
                    RaisePropertyChanged();
                }
            }
        }

        //private Handshake _selectedHandshake = Handshake.None;
        public int SelectedHandshake
        {
            get => (int)Settings.SelectedHandshake;
            set
            {
                var handshake = (int)value;
                if (Settings.SelectedHandshake != handshake)
                {
                    Settings.SelectedHandshake = handshake;
                    RaisePropertyChanged();
                }
            }
        }

        //private Parity _selectedParity = Parity.None;
        public int SelectedParity
        {
            get => (int)Settings.SelectedParity;
            set
            {
                var parity = (int)value;
                if (Settings.SelectedParity != parity)
                {
                    Settings.SelectedParity = parity;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<ActivityMode> ActivityModeOptions => new ObservableCollection<ActivityMode>
        {
            ActivityMode.String,
            ActivityMode.Hexadecimal,
        };

        //private ActivityMode _selectedActivityMode = ActivityMode.String;
        public ActivityMode SelectedActivityMode
        {
            get => (ActivityMode)Settings.SelectedActivityMode;
            set
            {
                var actMode = (int)value;
                if (Settings.SelectedActivityMode != actMode)
                {
                    Settings.SelectedActivityMode = actMode;
                    RaisePropertyChanged();
                }
            }
        }

        //private bool? _isCR = false;
        public bool IsCR
        {
            get => Settings.IsCR;
            set
            {
                if (Settings.IsCR != value)
                {
                    Settings.IsCR = value;
                    RaisePropertyChanged();
                }
            }
        }

        //private bool? _isLF = false;
        public bool IsLF
        {
            get => Settings.IsLF;
            set
            {
                if (Settings.IsLF != value)
                {
                    Settings.IsLF = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isRunning = false;
        public bool IsRunning
        {
            get => _isRunning;
            set => Set(ref _isRunning, value);
        }

        public bool CanRun
        {
            get => SerialPortList != null && SerialPortList.Count > 0;
        }

        //private string _sendCommandText;
        public string SendCommandText
        {
            get => Settings.SendMessage;
            set
            {
                if (Settings.SendMessage != value)
                {
                    Settings.SendMessage = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            _crossFileDialog = ServiceLocator.Current.Get<ICrossDialog>();
            _notification = ServiceLocator.Current.Get<INotificationService>();
            _systemService = ServiceLocator.Current.Get<ISystemService>();
            _file = ServiceLocator.Current.Get<IFileService>();
            _jsonSvc = ServiceLocator.Current.Get<IJsonService>();
            _enc = Encoding.UTF8;
            _engine = ServiceLocator.Current.Get<INetworkEngine>();
            _engine.MessageResponseReceived += (_, e) => ViewReceivedMessage(e.Data);
            //RefreshSerialPorts();
            MainForm_Load(null, null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            RefreshSerialPorts();

            SetSelectedPortLabel("-");
        }

        private string _runText = RunString;
        public string RunText
        {
            get => _runText;
            set => Set(ref _runText, value);
        }

        private string _connectedText = $"Connected Port:{Environment.NewLine}-";
        public string ConnectedText
        {
            get => _connectedText;
            set => Set(ref _connectedText, value);
        }

        private void SetSelectedPortLabel(string label)
        {
            ConnectedText = $"Connected Port: {label}";
        }

        public ICommand SendCommand => new Command(() =>
        {
            SendMessage(SelectedActivityMode == ActivityMode.String ? _enc.GetBytes(SendCommandText) : SerialDataConverter.HexStringToBytes(SendCommandText));
        });

        public virtual ICommand NewCommand => new Command(() =>
        {
            var model = CreateDefaultModel();
            UpdateFromModel(model);
            CurrentFilePath = string.Empty;
        });

        public virtual ICommand OpenCommand => new Command(async () =>
        {
            var filename = await _crossFileDialog.ShowOpenDialogAsync(FileExtenstion, FileTypeName);
            var model = LoadFromFile<TermSharpSettings>(filename);
            UpdateFromModel(model);
        });

        public virtual ICommand SaveCommand => new Command(async () =>
        {
            if (!_file.Exists(CurrentFilePath))
            {
                SaveAsCommand?.Execute(null);
                return;
            }

            var model = GenerateModel();
            await Task.Run(() => SaveToFile(model, CurrentFilePath));
        });

        public virtual ICommand SaveAsCommand => new Command(async () =>
        {
            var model = GenerateModel();
            var filename = await _crossFileDialog.ShowSaveDialogAsync(FileExtenstion, FileTypeName);
            SaveToFile(model, filename);
        });

        public TermSharpSettings CreateDefaultModel()
        {
            return new TermSharpSettings();
        }

        public TermSharpSettings GenerateModel()
        {
            return new TermSharpSettings
            {
                SelectedBaudRate = SelectedBaudRate,
                SelectedDataBits = SelectedDataBits,
                SelectedHandshake = SelectedHandshake,
                SelectedParity = SelectedParity,
                SelectedStopBits = SelectedStopBits,
                SelectedActivityMode = SelectedActivityMode,
                IsCR = IsCR,
                IsLF = IsLF,
            };
        }

        public void UpdateFromModel(TermSharpSettings m)
        {
            SelectedBaudRate = m.SelectedBaudRate;
            SelectedDataBits = m.SelectedDataBits;
            SelectedHandshake = m.SelectedHandshake;
            SelectedParity = m.SelectedParity;
            SelectedStopBits = m.SelectedStopBits;
            SelectedActivityMode = m.SelectedActivityMode;
            IsCR = m.IsCR;
            IsLF = m.IsLF;
        }

        public string FileExtenstion => "tcfg";

        public string FileTypeName => "TermSharp Configuration File";

        protected virtual T LoadFromFile<T>(string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename) && _file.Exists(filename))
            {
                CurrentFilePath = filename;
                var jsonString = _file.ReadAllText(CurrentFilePath);
                var model = _jsonSvc.Deserialize<T>(jsonString);
                return model;
            }

            return default(T);
        }

        protected virtual void SaveToFile<T>(T model, string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                CurrentFilePath = filename;
                var jsonString = _jsonSvc.Serialize(
                    model,
                    true);
                _file.WriteAllText(CurrentFilePath, jsonString);
            }
        }

        public string CurrentFilePath
        {
            get => Settings.CurrentSettingsFile;
            set
            {
                if (Settings.CurrentSettingsFile != value)
                {
                    Settings.CurrentSettingsFile = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public string Title
        {
            get => string.IsNullOrWhiteSpace(CurrentFilePath) ? $"{CoreApp.AssemblyProduct}" : $"{CoreApp.AssemblyProduct} ({CurrentFilePath})";
        }

        public ICommand ExitCommand => new Command(_systemService.Quit);

        public ICommand RefreshCommand => new Command(RefreshSerialPorts);

        public ICommand RunCommand => new Command(async () =>
        {
            try
            {
                if (RunText == RunString)
                {
                    if (SelectedSerialPortName == SerialPortList[0])
                    {
                        _engine.Set(new TcpListenerSettings
                        {
                            IpAddress = Settings.TcpClientIpAddress,
                            Port = Settings.TcpClientPort,
                        });
                    }
                    else if (SelectedSerialPortName == SerialPortList[1])
                    {
                        _engine.Set(new TcpClientSettings
                        {
                            IpAddress = Settings.TcpClientIpAddress,
                            Port = Settings.TcpClientPort,
                        });
                    }
                    else
                    {
                        _engine.Set(new SerialComSettings
                        {
                            PortName = SelectedSerialPortName,
                            BaudRate = Settings.SelectedBaudRate,
                            Handshake = (int)Settings.SelectedHandshake,
                            Parity = (int)Settings.SelectedParity,
                            DataBits = Settings.SelectedDataBits,
                            StopBits = (int)Settings.SelectedStopBits,
                            IsDtrEnable = Settings.IsDtrEnable,
                            IsRtsEnable = Settings.IsRtsEnable,
                        });
                    }

                    if (OpenSerialPort())
                    {
                        OnLogReceived(SelectedSerialPortName + " opened.");
                        SetSelectedPortLabel(SelectedSerialPortName);
                        RunText = StopString;
                        //buttonRun.Image = Resources.IconStop;
                        IsRunning = true;
                    }
                }
                else
                {
                    await Task.Run(() => CloseSerialPort());

                    OnLogReceived(SelectedSerialPortName + " closed.");
                    SetSelectedPortLabel("-");
                    RunText = RunString;
                    //buttonRun.Image = Resources.IconPlay;
                    IsRunning = false;
                    SendCommandText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                OnLogReceived(ex);
            }
        });

        private void RefreshSerialPorts()
        {
            SerialPortList = GetPortNames();
            if (SerialPortList.Count > 0)
            {
                SelectedSerialPortName = SerialPortList[0];
            }
            RaisePropertyChanged(nameof(CanRun));
            string jumlah = SerialPortList.Count > 0 ? SerialPortList.Count.ToString() : "No";
            _notification.Show("Serial Port", jumlah + " port(s) detected.");
        }

        private IList<string> GetPortNames()
        {
            return _systemService.GetAvailableNetworks();
        }

        /// <summary>
        /// Open serial port connection
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        private bool OpenSerialPort()
        {
            try
            {
                CloseSerialPort();
                _engine?.Open();
            }
            catch (Exception ex)
            {
                OnLogReceived(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// close serial port connection
        /// </summary>
        private void CloseSerialPort()
        {
            try
            {
                if (_engine.IsOpen)
                    _engine?.Close();
            }
            catch (Exception ex)
            {
                OnLogReceived(ex);
                return;
            }
        }

        /// <summary>
        /// Load app settings
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                if (Settings.DeviceBotEnabled)
                {
                    var type = (BotScriptType)Settings.SelectedScriptingLanguage;
                    if (type == BotScriptType.JavaScript)
                        DeviceBotEngine = ServiceLocator.Current.Get<IJavaScriptBotEngine>();
                    else
                        DeviceBotEngine = ServiceLocator.Current.Get<ICSharpBotEngine>();
                    DeviceBotEngine.Compile(Settings.CurrentBotScript);
                }
            }
            catch (Exception ex)
            {
                OnLogReceived(ex, "ERROR");
            }
        }

        private void ViewReceivedMessage(byte[] msgBytes)
        {
            var message = SelectedActivityMode == ActivityMode.String ? SerialDataConverter.BytesToString(msgBytes) : SerialDataConverter.BytesToHexString(msgBytes);
            OnLogReceived(message, "IN");
            if (Settings.DeviceBotEnabled)
            {
                var response = DeviceBotEngine.GetResponse(msgBytes);
                if (response != null && response.Length > 0)
                    SendMessage(response);
            }
        }

        private void SendMessage(byte[] data)
        {
            try
            {
                var dataList = new List<byte>(data);

                if (IsCR == true)
                    dataList.Add(0x0D);
                if (IsLF == true)
                    dataList.Add(0x0A);

                var dataToSend = dataList.ToArray();

                var logMessage = SelectedActivityMode == ActivityMode.String ? _enc.GetString(dataToSend, 0, dataToSend.Length) : SerialDataConverter.BytesToHexString(dataToSend);

                _engine.Write(dataToSend, 0, dataToSend.Length);

                OnLogReceived(logMessage, "OUT");
            }
            catch (Exception ex)
            {
                OnLogReceived(ex, "ERROR");
            }
        }

        public bool IsTopMost
        {
            get => Settings.MainFormTopMost;
            set
            {
                if (Settings.MainFormTopMost != value)
                {
                    Settings.MainFormTopMost = value;
                    RaisePropertyChanged();
                }
            }

        }
    }


}
