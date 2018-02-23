using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ItemSync2.Core
{
    class SQLDataConfig
    {
        private XmlDocument xml = new XmlDocument();
        string xmlPath;

        public string ID;
        public string ClassID;
        public string SubclassID;
        public string SoundOverrideSubclassID;
        public string Material;
        public string DisplayID;
        public string InventoryType;
        public string SheatheType;

        public Dictionary<string, string> defaultValues;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlPath">Path to SQLConfig.xml file.</param>
        public SQLDataConfig(string xmlPath)
        {
            this.xmlPath = xmlPath;
            LoadSettings();
        }

        /// <summary>
        /// Loads settings from XML specified in instance's xmlPath.
        /// </summary>
        private void LoadSettings()
        {
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException(string.Format("File {0} could not be found. Make sure its included in app's /Configs/ folder, ItemSync can't run without it.", xmlPath));

            try
            {
                using (var sr = new StreamReader(xmlPath))
                {
                    string xmlString = sr.ReadToEnd();
                    xml.LoadXml(xmlString);
                }

                ID = xml.GetElementsByTagName("ID")[0].InnerText;
                ClassID = xml.GetElementsByTagName("ClassID")[0].InnerText;
                SubclassID = xml.GetElementsByTagName("SubclassID")[0].InnerText;
                SoundOverrideSubclassID = xml.GetElementsByTagName("SoundOverrideSubclassID")[0].InnerText;
                Material = xml.GetElementsByTagName("Material")[0].InnerText;
                DisplayID = xml.GetElementsByTagName("DisplayID")[0].InnerText;
                InventoryType = xml.GetElementsByTagName("InventoryType")[0].InnerText;
                SheatheType = xml.GetElementsByTagName("SheatheType")[0].InnerText;

                defaultValues = new Dictionary<string, string>();
                foreach (XmlNode element in xml.GetElementsByTagName("DefaultValues")[0].ChildNodes)
                    if (element.NodeType == XmlNodeType.Element)
                        defaultValues.Add(element.Name, element.InnerText);
            }
            catch (Exception e) { throw new Exception(string.Format("Error occured while loading data from {0}.\n\n", xmlPath) + e.Message); }
        }
    }
}