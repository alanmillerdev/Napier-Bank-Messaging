using System;
using System.Windows.Input;

namespace NapierBankMessaging.Commands
{
    public class RelayCommand : ICommand
    {

        private Action _action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
