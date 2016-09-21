using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools.Helpers
{
    internal class NuGetPathResolver
    {
        public NuGetPathResolver(TaskLoggingHelper log, string solutionPath)
        {
            this.Log = log;
            this.SolutionPath = solutionPath;
        }

        public TaskLoggingHelper Log { get; private set; }
        public string SolutionPath { get; private set; }

        public string GetPath(string fileName)
        {
            string result = null;

            PackageConfigReader reader = new PackageConfigReader();

            var packages = reader.Read(fileName);
            if (packages != null && packages.Any())
            {
                var targets = (from o in packages
                               where o.Id == PackageId.NuGetCommandLine
                               select o);

                if (targets.Any())
                {
                    var package = (from o in targets
                                   orderby o.Version descending
                                   select o).FirstOrDefault();

                    if (targets.Count() > 1)
                    {
                        this.Log.LogWarning("Multiple {0} packages were installed, using version {1}.", PackageId.NuGetCommandLine, package.Version);
                    }

                    if (package != null)
                    {
                        result = Path.Combine(
                            this.SolutionPath,
                            "packages",
                            string.Format("{0}.{1}", package.Id, package.Version),
                            "tools");
                    }
                }
            }

            return result;
        }
    }
}