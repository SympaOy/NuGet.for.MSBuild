using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.MSBuild.Tools.Helpers;
using NuGet.MSBuild.Tools.Resolvers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NuGet.MSBuild.Tools
{
    public abstract class NuGetTask : ToolTask
    {
        public const string TOOL_NAME = @"NuGet.exe";

        [Required]
        public string SolutionPath { get; set; }

        protected override string ToolName
        {
            get { return TOOL_NAME; }
        }

        protected override string GenerateFullPathToTool()
        {
            string path = null;

            IPathResolutionService resolver = null;
            string root = Path.GetDirectoryName(this.SolutionPath);

            string packagesFolder = Path.Combine(root, NuGetConstants.PackagesFolder);
            if (Directory.Exists(packagesFolder))
            {
                resolver = new PackagesFolderResolver();
                path = resolver.GetPath(packagesFolder);
            }

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                this.Log.LogError(string.Format("The tool '{0}' location was not identified, or does not exist.", this.ToolName));
            }

            return path;
        }
    }
}