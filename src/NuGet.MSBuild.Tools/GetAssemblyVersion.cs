using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.MSBuild.Tools.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools
{
    public class GetAssemblyVersion : Task
    {
        [Required]
        public string FileName { get; set; }

        public bool UseFileVersion { get; set; }

        public bool UseInfoVersion { get; set; }

        [Output]
        public string Version { get; set; }

        public override bool Execute()
        {
            if (File.Exists(this.FileName))
            {
                AssemblyVersionResolver resolver = new AssemblyVersionResolver()
                {
                    UseFileVersion = this.UseFileVersion,
                    UseInfoVersion = this.UseInfoVersion
                };

                this.Version = resolver.GetVersion(this.FileName);
            }

            return true;
        }
    }
}
