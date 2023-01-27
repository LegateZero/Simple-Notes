using System;
using System.Windows.Input;

namespace Simple_Notes.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        public virtual bool CanExecute(object parameter) => true;

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;

        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
