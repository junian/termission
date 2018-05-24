using System;
using System.Linq;
using System.Reflection;

namespace Juniansoft.Termission.Core
{
    public static class CoreApp
    {
        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                var assembly = typeof(CoreApp)
                    .GetTypeInfo()
                    .Assembly;
                var attribute = assembly
                    .GetCustomAttributes<AssemblyTitleAttribute>()
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(attribute?.Title))
                {
                    return attribute.Title;
                }

                return assembly.FullName;
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                var assembly = typeof(CoreApp)
                    .GetTypeInfo()
                    .Assembly;
                return assembly.GetName().Version.ToString();
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                var assembly = typeof(CoreApp)
                    .GetTypeInfo()
                    .Assembly;
                var attribute = assembly
                    .GetCustomAttributes<AssemblyDescriptionAttribute>()
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(attribute?.Description))
                {
                    return attribute.Description;
                }

                return string.Empty;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                var assembly = typeof(CoreApp)
                    .GetTypeInfo()
                    .Assembly;
                var attribute = assembly
                    .GetCustomAttributes<AssemblyProductAttribute>()
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(attribute?.Product))
                {
                    return attribute.Product;
                }

                return string.Empty;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                var assembly = typeof(CoreApp)
                    .GetTypeInfo()
                    .Assembly;
                var attribute = assembly
                    .GetCustomAttributes<AssemblyCopyrightAttribute>()
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(attribute?.Copyright))
                {
                    return attribute.Copyright;
                }

                return string.Empty;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                var assembly = typeof(CoreApp)
                    .GetTypeInfo()
                    .Assembly;
                var attribute = assembly
                    .GetCustomAttributes<AssemblyCompanyAttribute>()
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(attribute?.Company))
                {
                    return attribute.Company;
                }

                return string.Empty;
            }
        }

        #endregion

    }
}
