using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.MSBuild.Tools
{
    public class Spec : NuGetTask
    {
        #region Properties

        [Required]
        public ITaskItem Project { get; set; }

        public string AssemblyPath { get; set; }

        public bool Force { get; set; }

        public string Verbosity { get; set; }

        #endregion

        protected override string GetWorkingDirectory()
        {
            return Path.GetDirectoryName(this.Project.ItemSpec);
        }

        protected override string GenerateCommandLineCommands()
        {
            CommandLineBuilder builder = new CommandLineBuilder();
            builder.AppendSwitch("spec");

            builder.AppendSwitchIfNotNull("-AssemblyPath ", this.AssemblyPath);
            builder.AppendSwitchIfNotNull("-Verbosity ", this.Verbosity);
            builder.AppendSwitchIfTrue("-Force", this.Force);
            builder.AppendSwitch("-NonInteractive");

            return builder.ToString();
        }
    }
}