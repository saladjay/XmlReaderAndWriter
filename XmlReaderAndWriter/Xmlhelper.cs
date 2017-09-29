using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlReaderAndWriter
{
    public static class Xmlhelper
    {
        public static void CreateXML(string Path,List<string> key, List<string> value,bool StandAlone=true)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode node = null;
            if (StandAlone)
                node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            else
                node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "no");
        }
    }
}
