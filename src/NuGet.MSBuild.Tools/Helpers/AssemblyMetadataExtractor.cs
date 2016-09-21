using NuGet.MSBuild.Tools.Resolvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NuGet.MSBuild.Tools.Helpers
{
    [Serializable]
    internal class AssemblyMetadata
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    internal class AssemblyMetadataExtractor
    {
        public AssemblyMetadata ExtractMetadata(string fileName, bool useFileVersion, bool useInfoVersion)
        {
            AssemblyName assemblyName = AssemblyName.GetAssemblyName(fileName);
            if (assemblyName != null)
            {
                AssemblyVersionResolver versionResolver = new AssemblyVersionResolver()
                {
                    UseFileVersion = useFileVersion,
                    UseInfoVersion = useInfoVersion
                };

                string version = versionResolver.GetVersion(fileName);

                return new AssemblyMetadata()
                {
                    Name = assemblyName.Name,
                    Version = version,
                };
            }

            return null;
        }
    }
}