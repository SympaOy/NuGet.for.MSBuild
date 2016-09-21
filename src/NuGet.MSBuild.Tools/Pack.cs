using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.MSBuild.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.MSBuild.Tools
{
    public class Pack : NuGetTask
    {
        #region Properties

        [Required]
        public string Target { get; set; }
        
        public string OutputDirectory { get; set; }

        public string BasePath { get; set; }

        public string Version { get; set; }

        public ITaskItem[] Excluded { get; set; }

        public bool Symbols { get; set; }

        public bool NoDefaultExcludes { get; set; }

        public bool NoPackageAnalysis { get; set; }

        public bool ExcludeEmptyDirectories { get; set; }

        public bool IncludeReferencedProjects { get; set; }

        public string MinClientVersion { get; set; }

        public ITaskItem[] Properties { get; set; }

        public string Verbosity { get; set; }

        public string CustomSwitches { get; set; }

        #endregion

        protected override string GenerateCommandLineCommands()
        {
            CommandLineBuilder builder = new CommandLineBuilder();
            builder.AppendSwitch("pack");

            if (!File.Exists(this.Target))
            {
                throw new FileNotFoundException(string.Format("The file '{0}' could not be found.", this.Target), this.Target);
            }

            builder.AppendFileNameIfNotNull(this.Target);
            builder.AppendSwitchIfTrue("-Symbols", this.Symbols);
            builder.AppendSwitchIfTrue("-NoDefaultExcludes", this.NoDefaultExcludes);
            builder.AppendSwitchIfTrue("-NoPackageAnalysis", this.NoPackageAnalysis);
            builder.AppendSwitchIfTrue("-ExcludeEmptyDirectories", this.ExcludeEmptyDirectories);
            builder.AppendSwitchIfTrue("-IncludeReferencedProjects", this.IncludeReferencedProjects);
            builder.AppendSwitchIfNotNull("-Version ", this.Version);
            builder.AppendSwitchIfNotNull("-Properties ", this.Properties, ";");
            builder.AppendSwitchIfNotNull("-Exclude ", this.Excluded, ";");
            builder.AppendSwitchIfNotNull("-MinClientVersion ", this.MinClientVersion);
            builder.AppendSwitchIfNotNull("-BasePath ", this.BasePath);
            builder.AppendSwitchIfNotNull("-OutputDirectory ", this.OutputDirectory);
            builder.AppendSwitchIfNotNull("-Verbosity ", this.Verbosity);
            builder.AppendSwitch("-NonInteractive ");
            builder.AppendTextUnquotedIfNotNullOrEmpty(this.CustomSwitches);

            return builder.ToString();
        }
    }
}
