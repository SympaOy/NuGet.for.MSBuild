using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.MSBuild.Tools.Resolvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.MSBuild.Tools.UnitTests
{
    [TestClass]
    public class PackagesFolderResolverTests
    {
        [TestMethod]
        public void GetPathTest()
        {
            PackagesFolderResolver target = new PackagesFolderResolver();
            var result = target.GetPath(@".\TestData\packages");

            Assert.IsNotNull(result);
        }
    }
}