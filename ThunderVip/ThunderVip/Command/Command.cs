using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThunderVip.Command
{
    class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> Copy;
        public Command(Action<object> copy)
        {
            this.Copy = copy;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Copy(parameter);
        }
    }
}
