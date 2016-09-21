using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.MSBuild.Tools
{
    public class Push : NuGetTask
    {
        #region Properties

        [Required]
        public ITaskItem PackagePath { get; set; }

        [Required]
        public string ApiKey { get; set; }

        public ITaskItem ConfigFile { get; set; }

        public string Source { get; set; }

        public string Verbosity { get; set; }

        #endregion

        protected override string GenerateCommandLineCommands()
        {
            CommandLineBuilder builder = new CommandLineBuilder();
            builder.AppendSwitch("push");

            builder.AppendFileNameIfNotNull(this.PackagePath);
            builder.AppendSwitchIfNotNull("-ApiKey ", this.ApiKey);
            builder.AppendSwitchIfNotNull("-ConfigFile ", this.ConfigFile);
            builder.AppendSwitchIfNotNull("-Source ", this.Source);
            builder.AppendSwitchIfNotNull("-Verbosity ", this.Verbosity);
            builder.AppendSwitch("-NonInteractive");

            return builder.ToString();
        }
    }
}
