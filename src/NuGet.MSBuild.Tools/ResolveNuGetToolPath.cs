using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.MSBuild.Tools.Resolvers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools
{
    public class ResolveNuGetToolPath : Task
    {
        [Required]
        public string Solution { get; set; }

        [Output]
        public string ToolPath { get; set; }

        public override bool Execute()
        {
            string path = GetPathForTool();
            if (!string.IsNullOrEmpty(path) && File.Exists(Path.Combine(path, NuGetTask.TOOL_NAME)))
            {
                this.ToolPath = path;
                return true;
            }

            return false;
        }

        private string GetPathForTool()
        {
            IPathResolutionService resolver = null;
            string root = Path.GetDirectoryName(this.Solution);

            string packagesFolder = Path.Combine(root, "packages");
            if (Directory.Exists(packagesFolder))
            {
                resolver = new PackagesFolderResolver(this.Log);
                return resolver.GetPath(packagesFolder);
            }

            return null;
        }
    }
}