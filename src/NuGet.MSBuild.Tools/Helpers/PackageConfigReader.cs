using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace NuGet.MSBuild.Tools.Helpers
{
    internal class Package
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public bool DevelopmentDependency { get; set; }
    }

    internal class PackageConfigReader
    {
        public PackageConfigReader()
        {
        }

        public IEnumerable<Package> Read(string fileName)
        {
            ObservableCollection<Package> result = new ObservableCollection<Package>();

            XmlDocument document = new XmlDocument();
            document.Load(fileName);

            var root = document["packages"];

            foreach (XmlNode child in root.ChildNodes)
            {
                Package package = new Package()
                {
                    Id = child.Attributes["id"].Value,
                    Version = child.Attributes["version"].Value
                };

                if (child.Attributes["developmentDependency"] != null)
                {
                    package.DevelopmentDependency = bool.Parse(child.Attributes["developmentDependency"].Value);
                }

                result.Add(package);
            }

            return result;
        }
    }
}