using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools.Resolvers
{
    internal interface IPathResolutionService
    {
        string GetPath(string value);
    }
}