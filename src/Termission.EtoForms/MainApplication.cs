using System;
using System.Collections.Generic;
using System.Reflection;
using Eto;
using Eto.Forms;
using Juniansoft.Termission.Core;
using Juniansoft.Termission.Core.Engines.Networks;
using Juniansoft.Termission.Core.Engines.Scripts;
using Juniansoft.Termission.Core.Services;
using Juniansoft.Termission.Core.ViewModels;
using Juniansoft.Termission.EtoForms.Forms;
using Juniansoft.Termission.EtoForms.Services;
using Juniansoft.Termission.EtoForms.Views;
using Mono.Options;

namespace Juniansoft.Termission.EtoForms
{
    public class MainApplication: Application, ITermissionApp
    {
        public MainApplication(Platform platform)
            : base(platform)
        {
            RegisterServices();
        }

        public void Run(string[] args)
        {
            // these variables will be set when the command line is parsed
            var verbosity = 0;
            var shouldShowHelp = false;

            // these are the available options, not that they set the variables
            var options = new OptionSet {
                { 
                    "v", "increase debug message verbosity", v => {
                    if (v != null)
                        ++verbosity;
                    } 
                },
                {
                    "h|help", "show this message and exit", h => shouldShowHelp = h != null
                },
                // default
                { "<>", v =>
                    {
                        //shouldShowHelp = true;
                        //Console.WriteLine("Unknown command parameter.");
                    }
                },
            };

            var extra = default(List<string>);
            try
            {
                // parse the command line
                extra = options.Parse(args);
            }
            catch (OptionException e)
            {
                // output some error message
                Console.Write("termission: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `termission --help' for more information.");
                return;
            }

            if (shouldShowHelp)
            {
                // show some app description message
                Console.WriteLine("Usage: termission.exe [OPTIONS]");
                Console.WriteLine("Cross-platform Serial/TCP terminal.");
                Console.WriteLine();

                // output the options
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            this.Run(new MainForm());
        }

        public void RegisterServices()
        {
            // Register all Services here
            ServiceLocator.Current.Register<ICrossDialog, CrossDialog>();
            ServiceLocator.Current.Register<IJavaScriptBotEngine, JSJintScriptEngine>();
            ServiceLocator.Current.Register<INetworkEngine, TermSharpEngine>();
            ServiceLocator.Current.Register<ISystemService, EtoSystemService>();
            ServiceLocator.Current.Register<IFileService, FileService>();

            if (Platform.Instance.IsWinForms || Platform.IsWpf)
                ServiceLocator.Current.Register<ICSharpBotEngine, CSharpCodeDomScriptEngine>();

            // Register all ViewModels here.
            ServiceLocator.Current.Register<MainViewModel>();
            ServiceLocator.Current.Register<DeviceBotViewModel>();
            ServiceLocator.Current.Register<PreferencesViewModel>();
            ServiceLocator.Current.Register<HelpViewModel>();

            // Register all Views here
            ServiceLocator.Current.Register<MainView>();
            ServiceLocator.Current.Register<DeviceBotView>();
            ServiceLocator.Current.Register<PreferencesView>();
        }

        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion
    }
}
