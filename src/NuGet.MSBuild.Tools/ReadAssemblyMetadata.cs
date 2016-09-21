using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.MSBuild.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools
{
    public class ReadAssemblyMetadata : Task
    {
        [Required]
        public string Assembly { get; set; }

        public bool UseFileVersion { get; set; }

        public bool UseInfoVersion { get; set; }

        [Output]
        public string AssemblyVersion { get; set; }

        public override bool Execute()
        {
            try
            {
                var extractor = new AssemblyMetadataExtractor();
                var metadata = extractor.ExtractMetadata(this.Assembly, this.UseFileVersion, this.UseInfoVersion);
                if (metadata != null)
                {
                    this.AssemblyVersion = metadata.Version;

                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Log.LogErrorFromException(ex, true);
            }

            return false;
        }
    }
}