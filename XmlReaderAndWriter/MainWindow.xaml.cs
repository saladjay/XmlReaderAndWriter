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

namespace XmlReaderAndWriter
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        SaveFileDialog saveDlg = new SaveFileDialog();
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
                XmlNode node = null;
                node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                XmlNode root = xmlDoc.CreateElement("Config");
                xmlDoc.AppendChild(root);
                xmlDoc.Save(strp);

                // BitConverter.ToString(mFactoryData);//for show...
                // textBx.AppendText("\n");  
                // textBx.AppendText(strp);
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
    }
}
