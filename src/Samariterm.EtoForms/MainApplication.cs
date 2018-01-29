﻿using System;
using System.Reflection;
using Eto;
using Eto.Forms;
using Juniansoft.Samariterm.Core;
using Juniansoft.Samariterm.Core.Engines.Networks;
using Juniansoft.Samariterm.Core.Engines.Scripts;
using Juniansoft.Samariterm.Core.Services;
using Juniansoft.Samariterm.Core.ViewModels;
using Juniansoft.Samariterm.EtoForms.Services;
using Juniansoft.Samariterm.EtoForms.Views;

namespace Juniansoft.Samariterm.EtoForms
{
    public class MainApplication: Application, ISamaritermApp
    {
        public MainApplication(Platform platform)
            : base(platform)
        {
            RegisterServices();
        }

        public void RegisterServices()
        {
            // Register all Services here
            ServiceLocator.Instance.Register<ICrossDialog, CrossDialog>();
            ServiceLocator.Instance.Register<IJavaScriptBotEngine, JSJintScriptEngine>();
            ServiceLocator.Instance.Register<INetworkEngine, TermSharpEngine>();
            ServiceLocator.Instance.Register<ISystemService, EtoSystemService>();
            ServiceLocator.Instance.Register<IFileService, FileService>();

            if (Platform.Instance.IsWinForms || Platform.IsWpf)
                ServiceLocator.Instance.Register<ICSharpBotEngine, CSharpCodeDomScriptEngine>();

            // Register all ViewModels here.
            ServiceLocator.Instance.Register<BaseViewModel>();
            ServiceLocator.Instance.Register<DeviceBotViewModel>();
            ServiceLocator.Instance.Register<PreferencesViewModel>();

            // Register all Views here
            ServiceLocator.Instance.Register<MainView>();
            ServiceLocator.Instance.Register<DeviceBotView>();
            ServiceLocator.Instance.Register<PreferencesView>();
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
