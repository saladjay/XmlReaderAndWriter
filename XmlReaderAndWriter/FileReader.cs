using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReaderAndWriter
{
    public class FileReader
    {
        public static string ReadTxt(string path)
        {
            StringBuilder StrTxt = new StringBuilder();
            StreamReader streamReader = new StreamReader(path,Encoding.Default);
            StrTxt.Append(streamReader.ReadToEnd());
            return StrTxt.ToString();
        }
    }
}
