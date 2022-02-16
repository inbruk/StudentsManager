using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using System.Reflection;

namespace StudentsManager.BusinessLogicLayer
{
    public static class WebConfigTools
    {
        private static XDocument configDoc = null;
        public static String ConfigFilePathName { set; get; }
        
        private static void checkAndLoadConfigDocument()
        {
            if (configDoc == null)
            {
                try
                {
                    configDoc = XDocument.Load(ConfigFilePathName);
                }
                catch (System.IO.FileNotFoundException e)
                {
                    throw new Exception("Конфигурационный файл не удалось загрузить", e);
                }
            }
        }

        public static String ReadConnectionStringByName(String name)
        {
            checkAndLoadConfigDocument();

            XElement currConnStrElem =
                configDoc.Elements("configuration").Elements("connectionStrings").Elements("add").Single(x => (String)x.Attribute("name") == "StudentsManagerEntities");

            String result = (String)currConnStrElem.Attribute("connectionString");
            return result;
        }

        public static void UpdateConnectionStringByName(String name, String newValue)
        {
            checkAndLoadConfigDocument();

            try
            {
                XElement currConnStrElem =
                    configDoc.Elements("configuration").Elements("connectionStrings").Elements("add").Single(x => (String)x.Attribute("name") == "StudentsManagerEntities");

                XAttribute currAttr = currConnStrElem.Attribute("connectionString");
                currAttr.Value = newValue;

                configDoc.Save(ConfigFilePathName);
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("Конфигурационный файл не удалось сохранить", e);
            }
        }
    }
}
