using Juniansoft.MvvmReady;
using Juniansoft.Termission.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Juniansoft.Termission.Core.ViewModels
{
    public class PreferencesViewModel : CoreViewModel
    {
        public string TcpListenerIp
        {
            get => Settings.TcpListenerIpAddress;
            set
            {
                if (Settings.TcpListenerIpAddress != value)
                {
                    Settings.TcpListenerIpAddress = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int TcpListenerPort
        {
            get => Settings.TcpListenerPort;
            set
            {
                if (Settings.TcpListenerPort != value)
                {
                    Settings.TcpListenerPort = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string TcpClientIp
        {
            get => Settings.TcpClientIpAddress;
            set
            {
                if (Settings.TcpClientIpAddress != value)
                {
                    Settings.TcpClientIpAddress = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int TcpClientPort
        {
            get => Settings.TcpClientPort;
            set
            {
                if (Settings.TcpClientPort != value)
                {
                    Settings.TcpClientPort = value;
                    RaisePropertyChanged();
                }
            }
        }

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

        public bool IsDtrEnable
        {
            get => Settings.IsDtrEnable;
            set
            {
                var dtr = value;
                if (Settings.IsDtrEnable != dtr)
                {
                    Settings.IsDtrEnable = dtr;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsRtsEnable
        {
            get => Settings.IsRtsEnable;
            set
            {
                var rts = value;
                if (Settings.IsRtsEnable != rts)
                {
                    Settings.IsRtsEnable = rts;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand ResetTcpListenerCommand => new Command(() => 
        {
            TcpListenerIp = Settings.DefaultTcpListenerIpAddress;
            TcpListenerPort = Settings.DefaultTcpListenerPort;
        });

        public ICommand ResetTcpClientCommand => new Command(() => 
        {
            TcpClientIp = Settings.DefaultTcpClientIpAddress;
            TcpClientPort = Settings.DefaultTcpClientPort;
        });

        public ICommand ResetSerialComCommand => new Command(() => 
        {
            SelectedBaudRate = Settings.DefaultBaudRate;
            SelectedParity = Settings.DefaultParity;
            SelectedDataBits = Settings.DefaultDataBits;
            SelectedStopBits = Settings.DefaultStopBits;
            SelectedHandshake = Settings.DefaultHandshake;
            IsDtrEnable = Settings.DefaultDtrEnable;
            IsRtsEnable = Settings.DefaultRtsEnable;
        });

        public ICommand ResetAllCommand => new Command(() => 
        {
            ResetTcpListenerCommand.Execute(null);

            ResetTcpClientCommand.Execute(null);

            ResetSerialComCommand.Execute(null);
        });

        public PreferencesViewModel()
        {

        }
    }
}
