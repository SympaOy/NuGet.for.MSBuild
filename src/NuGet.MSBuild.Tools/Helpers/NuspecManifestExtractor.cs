using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NuGet.MSBuild.Tools.Helpers
{
    [Serializable]
    [XmlRoot("package")]
    public class NuspecManifest
    {
        [XmlElement("metadata")]
        public NuspecManifestMetadata Metadata { get; set; }
    }

    [Serializable]
    public class NuspecManifestMetadata
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }
    }

    internal class NuspecManifestExtractor
    {
        public NuspecManifest ReadManifest(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(string.Format("The file '{0}' could not be found.", fileName), fileName);
            }

            string xmlns = DetermineNamespace(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(NuspecManifest), xmlns);

            using (Stream inStream = File.OpenRead(fileName))
            {
                inStream.Seek(0, SeekOrigin.Begin);

                using (XmlReader reader = XmlReader.Create(inStream))
                {
                    return (NuspecManifest)serializer.Deserialize(reader);
                }
            }            
        }

        private static string DetermineNamespace(string fileName)
        {
            string result = string.Empty;

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            // Since the namespace could be optional (depending on who wrote it) don't depend on it being there.
            if (doc.DocumentElement != null && !string.IsNullOrEmpty(doc.DocumentElement.NamespaceURI))
            {
                result = doc.DocumentElement.NamespaceURI;
            }

            return result;
        }
    }
}
