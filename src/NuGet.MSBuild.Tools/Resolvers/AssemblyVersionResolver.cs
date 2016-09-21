using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NuGet.MSBuild.Tools.Resolvers
{
    internal class AssemblyVersionResolver
    {
        public bool UseFileVersion { get; set; }
        public bool UseInfoVersion { get; set; }

        public string GetVersion(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("fileName cannot be an empty string or null reference.", "fileName");
            }

            string result = null;

            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(fileName);
            if (fvi != null)
            {
                if (!string.Equals(fvi.FileVersion, fvi.ProductVersion, StringComparison.CurrentCultureIgnoreCase) && this.UseInfoVersion)
                {
                    // A semantic version attribute was applied to the assembly, use it.
                    result = fvi.ProductVersion;
                }
                else if (this.UseFileVersion)
                {
                    result = fvi.FileVersion;
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                AssemblyName name = AssemblyName.GetAssemblyName(fileName);
                if (name != null)
                {
                    result = name.Version.ToString();
                }
            }

            return result;
        }
    }
}