using System.Windows.Input;

namespace FunctionCalculator.ViewModel
{
    /// <summary>
    /// Реализация команды.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Действие, выполняемое командой.
        /// </summary>
        private readonly Action _execute;

        /// <summary>
        /// Функция, определяющая, может ли команда выполняться.
        /// </summary>
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Создает экземпляр класса <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">Действие, выполняемое командой.</param>
        /// <param name="canExecute">Функция, определяющая,
        /// может ли команда выполняться.</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Определяет, может ли команда выполняться.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        /// <returns>True, если команда может выполняться,
        /// иначе False.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        public void Execute(object parameter)
        {
            _execute();
        }

        /// <summary>
        /// Событие, уведомляющее об изменении состояния выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Уведомляет подписчиков об изменении состояния выполнения команды.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
