using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XmlReaderAndWriter
{
    public class DelegateCommand : ICommand
    {
        public Action ExecuteCommand = null;

        public Func<bool> CanExecuteCommand = null;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (CanExecuteCommand != null)
            {
                return this.CanExecuteCommand();
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            this.ExecuteCommand?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


    }


}
