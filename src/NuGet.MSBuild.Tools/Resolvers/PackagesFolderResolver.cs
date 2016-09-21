using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools.Resolvers
{
    internal class PackagesFolderResolver : IPathResolutionService
    {
        public string GetPath(string value)
        {
            value = Path.GetFullPath(value);

            string result = null;
            var folders = (from o in Directory.EnumerateDirectories(value)
                           orderby o descending
                           select new DirectoryInfo(o)).ToArray();

            foreach (var folder in folders)
            {
                if (!folder.Name.StartsWith(NuGetConstants.NuGetCommandLine + "."))
                {
                    continue;
                }

                var tool = Path.Combine(folder.FullName, NuGetConstants.ToolsFolder, NuGetTask.TOOL_NAME);
                if (File.Exists(tool))
                {
                    result = tool;
                    break;
                }
            }

            return result;
        }
    }
}