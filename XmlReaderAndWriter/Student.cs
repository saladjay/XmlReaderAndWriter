using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReaderAndWriter
{
    public class Student:NotificationObject
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                this.RaisePropertyChanged("ID");
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                this.RaisePropertyChanged("Name");
            }
        }
    }
}
