using System;
using System.Windows.Input;
namespace Converter.Core
{
    // we created this class in order to create commands and bind them to buttons
    public class RelayCommand<T> : ICommand
    {
        public Action<T> _execute;        
        public RelayCommand(Action<T> execute)
        {
            this._execute = execute;           
        }
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object? parameter)
        {
            return true ;
        }
        public void Execute(object? parameter = null)
        {
            this._execute((T)Convert.ChangeType(parameter, typeof(T)));
        }
    }
}