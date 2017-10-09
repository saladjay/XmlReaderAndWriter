using ExtendedString;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlReaderAndWriter
{
    public class ViewModel:NotificationObject
    {

        public Student SelectedStudent { get; set; }

        private DelegateCommand _RandomSelection = new DelegateCommand();

        public DelegateCommand RandomSelection
        {
            get { return _RandomSelection; }
        }

        private DelegateCommand _LoadTxt = new DelegateCommand();

        public DelegateCommand LoadTxt
        {
            get { return _LoadTxt; }
        }

        private Xmlhelper _StudentXml = null;
        OpenFileDialog openDlg = new OpenFileDialog();
        public ViewModel()
        {
            SelectedStudent = new Student();
            _RandomSelection.ExecuteCommand += RandomSelectionExecute;
            _RandomSelection.CanExecuteCommand += RandomSelectionCanExecute;
            _LoadTxt.ExecuteCommand += LoadTxtExecute;
            _StudentXml = new Xmlhelper(System.AppDomain.CurrentDomain.BaseDirectory + "StudentsXml.xml");
            InitialViewModel();
        }

        private void RandomSelectionExecute()
        {
            int RandomResult = _Random.Next(Limit);
            int SelectedIndex = -1;
            do
            {
                SelectedIndex++;
                if (_StudentStatesList[SelectedIndex])
                {
                    RandomResult--;
                }
            } while (RandomResult >= 0);

            SelectedStudent.Name = _StudentNamesList[SelectedIndex];
            Limit--;
            if (Limit == 0)
            {
                Limit = _StudentStatesList.Count;
                for (int i = 0; i < _StudentStatesList.Count; i++)
                {
                    _StudentStatesList[i] = true;
                }
                _StudentXml.RefreshStudentState();
                Debug.WriteLine("Refresh");
            }
            else
            {
                _StudentXml.SingleStudentStateChanged(SelectedIndex);
                _StudentStatesList[SelectedIndex] = false;
            }
        }

        private bool RandomSelectionCanExecute()
        {
            return File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "StudentsXml.xml");
        }

        private void LoadTxtExecute()
        {
            openDlg.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            openDlg.Filter = "StudentName File|*.txt";
            if(openDlg.ShowDialog()==true)
            {
                string NameTxt = FileReader.ReadTxt(openDlg.FileName);
                string[] tempArray = NameTxt.Split('\r');
                for (int i = 0; i < tempArray.Count(); i++)
                {
                    tempArray[i] = tempArray[i].CStrRemoveNullandEmptyAndReturn();
                }
                _StudentXml.CreateStudentXml(tempArray);
                _RandomSelection.RaiseCanExecuteChanged();
            }
        }

        private List<bool> _StudentStatesList = new List<bool>();
        private List<string> _StudentNamesList = new List<string>();
        private Random _Random = new Random();
        private int Limit = -1;
        private void InitialViewModel()
        {
            if (!File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "StudentsXml.xml"))
            {
                return;
            }
            XmlDocument StudentsXml = _StudentXml.LoadStudentXml();
            XmlElement root = (XmlElement)StudentsXml.SelectSingleNode("Class");
            if (root != null && root.HasChildNodes)
            {
                Limit = 0;
                foreach (XmlNode Student in root.ChildNodes)
                {
                    XmlElement StudentName = (XmlElement)Student.FirstChild;
                    XmlElement StudentState = (XmlElement)Student.LastChild;
                    _StudentNamesList.Add(StudentName.InnerText);
                    if (StudentState.InnerText == "True")
                    {
                        _StudentStatesList.Add(true);
                        Limit++;
                    }
                    else
                    {
                        _StudentStatesList.Add(false);
                    }
                }
            }
        }
    }
}
