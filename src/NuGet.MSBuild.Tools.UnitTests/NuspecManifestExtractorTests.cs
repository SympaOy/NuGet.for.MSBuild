using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.MSBuild.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.MSBuild.Tools.UnitTests
{
    [TestClass]
    public class NuspecManifestExtractorTests
    {
        [TestMethod]
        [DeploymentItem(@"..\..\TestData\Test.nuspec")]
        public void ExtractTest()
        {
            NuspecManifestExtractor target = new NuspecManifestExtractor();
            var manifest = target.ReadManifest(@".\Test.nuspec");
        }
    }
}