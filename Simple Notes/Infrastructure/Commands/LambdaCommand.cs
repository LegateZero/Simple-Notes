using System;
using Simple_Notes.Infrastructure.Commands.Base;

namespace Simple_Notes.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = p =>
            {
                if (canExecute == null) return true;
                return canExecute(p);
            };
        }

        public LambdaCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = _ => execute.Invoke();
            _canExecute = _ =>
            {
                if (canExecute == null) return true;
                return canExecute();
            };
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public override void Execute(object parameter) => _execute(parameter);
    }
}
