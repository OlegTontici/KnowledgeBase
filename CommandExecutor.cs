using System;
using System.Windows.Input;

namespace KnowledgeBase
{
    public class CommandExecutor : ICommand
    {
        private readonly Action _actionToExecute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public CommandExecutor(Action actionToExecute, Func<bool> canExecute)
        {
            _actionToExecute = actionToExecute;
            _canExecute = canExecute;
        }

        public CommandExecutor(Action actionToExecute) : this(actionToExecute, () => true)
        {

        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _actionToExecute();
        }
    }
    public class CommandExecutor<T> : ICommand
    {
        private readonly Action<T> _actionToExecute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public CommandExecutor(Action<T> actionToExecute, Func<bool> canExecute)
        {
            _actionToExecute = actionToExecute;
            _canExecute = canExecute;
        }

        public CommandExecutor(Action<T> actionToExecute) : this(actionToExecute, () => true)
        {

        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _actionToExecute((T)parameter);
        }
    }
}
