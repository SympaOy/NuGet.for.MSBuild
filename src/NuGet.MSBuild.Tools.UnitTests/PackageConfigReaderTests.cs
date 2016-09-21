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
    public class PackageConfigReaderTests
    {
        [TestMethod]
        [DeploymentItem(@"..\..\TestData\packages.config")]
        public void ReaderTest()
        {
            PackageConfigReader target = new PackageConfigReader();
            var packages = target.Read(@".\packages.config");

            var package = packages.Where(o => o.Id == PackageId.NuGetCommandLine).Single();
        }
    }
}