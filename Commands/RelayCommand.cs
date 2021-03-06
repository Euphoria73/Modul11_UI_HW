using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Modul11_UI_HW.Commands
{
    class RelayCommand : ICommand
    {              
        private Action<object> execute;
        private Func<object, bool> canExecute;

        /// <summary>
        /// Контроль событий в WPF
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; //вызываем метод Delegate.Combine для добавления нового метода в список вызовов
            remove => CommandManager.RequerySuggested -= value; //вызываем метод Delegate.Remove для удаления метода из списка вызовов. 
                                                                //Если список пустой, то присвоится null
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// выполняет логику команды
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        /// <summary>
        /// определяет, может ли команда выполняться
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
    }

}

