using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlReaderAndWriter
{
    public class Xmlhelper
    {
        private string _FilePath;

        public Xmlhelper(string FilePath)
        {
            _FilePath = FilePath;
        }

        public XmlDocument LoadStudentXml()
        {
            XmlDocument StudentXml = new XmlDocument();
            StudentXml.Load(_FilePath);
            return StudentXml;
        }

        public void CreateStudentXml(string[] NameList)
        {
            XmlDocument StudentXml = new XmlDocument();
            XmlNode Declaration = StudentXml.CreateXmlDeclaration("1.0", "utf-8", "yes");
            StudentXml.AppendChild(Declaration);
            XmlNode Class = StudentXml.CreateElement("Class");
            StudentXml.AppendChild(Class);
            for (int i = 0; i < NameList.Count(); i++)
            {
                XmlElement student = StudentXml.CreateElement("student");
                student.SetAttribute("ID", i.ToString());

                XmlElement studentName = StudentXml.CreateElement("Name");
                studentName.AppendChild(StudentXml.CreateTextNode(NameList[i]));
                student.AppendChild(studentName);

                XmlElement studentState = StudentXml.CreateElement("State");
                studentState.AppendChild(StudentXml.CreateTextNode("True"));
                student.AppendChild(studentState);

                Class.AppendChild(student);
            }
            StudentXml.Save(_FilePath);
        }

        public void SingleStudentStateChanged(int ID)
        {
            XmlDocument StudentXml = new XmlDocument();
            StudentXml.Load(_FilePath);
            XmlElement selectedEle = (XmlElement)StudentXml.DocumentElement.SelectSingleNode("/Class/student[@ID='1']");
            XmlElement state = (XmlElement)selectedEle.GetElementsByTagName("State")[0];
            state.InnerText = "False";
            StudentXml.Save(_FilePath);
        }

        public void RefreshStudentState()
        {
            XmlDocument StudentXml = new XmlDocument();
            StudentXml.Load(_FilePath);
            foreach (XmlElement Student in StudentXml.SelectSingleNode("Class").ChildNodes)
            {
                Student.LastChild.InnerText = "True";
            }
            StudentXml.Save(_FilePath);
        }
    }
}
