using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ExtendedString;

namespace XmlReaderAndWriter
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        SaveFileDialog saveDlg = new SaveFileDialog();
        OpenFileDialog openDlg = new OpenFileDialog();
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            saveDlg.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            saveDlg.Filter = "FactoryPresets File|*.xml";
            saveDlg.OverwritePrompt = true;
            if (saveDlg.ShowDialog() == true)
            {
                string strp = saveDlg.FileName;
                Debug.WriteLine(strp);
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode Declaration = null;
                Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                xmlDoc.AppendChild(Declaration);
                XmlNode root = xmlDoc.CreateElement("Class");
                xmlDoc.AppendChild(root);
                XmlElement student1 = xmlDoc.CreateElement("student");
                student1.SetAttribute("ID", "1");

                XmlElement studentName = xmlDoc.CreateElement("Name");
                studentName.AppendChild(xmlDoc.CreateTextNode("abc"));
                student1.AppendChild(studentName);

                XmlElement studentState = xmlDoc.CreateElement("State");
                studentState.AppendChild(xmlDoc.CreateTextNode("false"));
                student1.AppendChild(studentState);
                root.AppendChild(student1);


                XmlElement selectedEle = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("/Class/student[@ID='1']");
                XmlElement state = (XmlElement)selectedEle.GetElementsByTagName("State")[0];
                state.InnerText = "True";
                xmlDoc.Save(strp);

            }
            {
                string strp = System.AppDomain.CurrentDomain.BaseDirectory;
                strp += "456.xml";
                Debug.WriteLine(strp);
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode node = null;
                node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                XmlNode root = xmlDoc.CreateElement("Config");
                xmlDoc.AppendChild(root);
                xmlDoc.Save(strp);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string strp = System.AppDomain.CurrentDomain.BaseDirectory;
            strp += "123456.txt";
            string aa = FileReader.ReadTxt(strp);
            Debug.WriteLine(aa);
            string[] tempArray = aa.Split('\r');
            foreach (string item in tempArray)
            {    
                Debug.WriteLine("|"+item.CStrRemoveNullandEmptyAndReturn() + "|");
            }
        }
    }
}
