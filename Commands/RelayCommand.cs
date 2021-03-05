using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Modul11_UI_HW.Commands
{
    internal abstract class RelayCommand : ICommand
    {
        /// <summary>
        /// Контроль событий в WPF
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; //вызываем метод Delegate.Combine для добавления нового метода в список вызовов
            remove => CommandManager.RequerySuggested -= value; //вызываем метод Delegate.Remove для удаления метода из списка вызовов. Если список пустой, то присвоится null
        }

        /// <summary>
        /// определяет, может ли команда выполняться
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract bool CanExecute(object parameter);


        /// <summary>
        /// выполняет логику команды
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);
    }
}
