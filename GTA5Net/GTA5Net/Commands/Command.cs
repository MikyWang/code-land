using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Commands.Command
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action execute;
        private Func<bool> canExecute = null;
        private DispatcherTimer dispatcherTimer = null;
        public Command(Action excute, Func<bool> canExecute)
        {
            this.execute = excute;
            this.canExecute = canExecute;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += canExecuteTime_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        public void Execute(object parameter)
        {
            execute();
        }
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            return canExecute();
        }
        private void canExecuteTime_Tick(object sender, object e)
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
