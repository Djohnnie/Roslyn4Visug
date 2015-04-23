using System;
using System.Windows.Input;

namespace Roslyn.Visug.NewCSharpFeatures.NameOfExpression
{
    public class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _action;

        public ActionCommand(Action action)
        {
            _action = action;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            _action();
        }
    }
}
