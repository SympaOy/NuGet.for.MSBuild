using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.MSBuild.Tools.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.MSBuild.Tools.UnitTests
{
    [TestClass]
    public class AssemblyVersionResolverTests
    {
        [TestMethod]
        public void GetVersionTest()
        {
            AssemblyVersionResolver target = new AssemblyVersionResolver()
            {
                UseFileVersion = false
            };

            string result = target.GetVersion(@".\NuGet.MSBuild.Tools.dll");
        }
    }
}