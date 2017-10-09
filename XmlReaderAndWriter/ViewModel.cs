using ExtendedString;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            SelectioinTimer.Elapsed += SelectioinTimer_Elapsed;
        }

        private void SelectioinTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (NameQueue.Count==1)
            {
                SelectioinTimer.Stop();
                SelectedStudent.Name = NameQueue.Dequeue();
            }
            else
            {
                SelectedStudent.Name = NameQueue.Dequeue();
            }
        }

        private void RandomSelectionExecute()
        {
            int RandomResult = 0;
            int SelectedIndex = -1;
            int SelectionTimes = _RandomTimes.Next(1,8);
            for (int i = 0; i < SelectionTimes; i++)
            {
                SelectedIndex = -1;
                RandomResult = _Random.Next(Limit);
                do
                {
                    SelectedIndex++;
                    if (_StudentStatesList[SelectedIndex])
                    {
                        RandomResult--;
                    }
                } while (RandomResult >= 0);

                //SelectedStudent.Name = _StudentNamesList[SelectedIndex];
                NameQueue.Enqueue(_StudentNamesList[SelectedIndex]);

            }
            SelectioinTimer.Start();
            Thread.Sleep(200);
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

        private bool RandomSelectionCanExecute(object param=null)
        {
            if (param == null)
                return File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "StudentsXml.xml");
            else
                return (bool)param;
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
                InitialViewModel();
            }
        }

        private List<bool> _StudentStatesList = new List<bool>();
        private List<string> _StudentNamesList = new List<string>();
        private Random _Random = new Random();
        private Random _RandomTimes = new Random();
        private System.Timers.Timer SelectioinTimer = new System.Timers.Timer(200);
        private Queue<string> NameQueue = new Queue<string>();
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
