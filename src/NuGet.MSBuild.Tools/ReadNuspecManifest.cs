using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.MSBuild.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools
{
    public class ReadNuspecManifest : Task
    {
        [Required]
        public string Manifest { get; set; }

        [Output]
        public string PackageId { get; set; }

        [Output]
        public string PackageVersion { get; set; }

        public override bool Execute()
        {
            try
            {
                var extractor = new NuspecManifestExtractor();
                var manifest = extractor.ReadManifest(this.Manifest);
                if (manifest != null)
                {
                    this.PackageId = manifest.Metadata.Id;
                    this.PackageVersion = manifest.Metadata.Version;

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