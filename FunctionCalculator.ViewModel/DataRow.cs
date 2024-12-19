using System.ComponentModel;
using System.Runtime.CompilerServices;
using FunctionCalculator.Model;

namespace FunctionCalculator.ViewModel
{
    /// <summary>
    /// Представляет строку данных таблицы.
    /// </summary>
    public class DataRow : INotifyPropertyChanged
    {
        /// <summary>
        /// Значение X.
        /// </summary>
        private double _x;

        /// <summary>
        /// Значение Y.
        /// </summary>
        private double _y;

        /// <summary>
        /// Рузультат.
        /// </summary>
        private double _result;

        /// <summary>
        /// Функция.
        /// </summary>
        private FunctionBase _function;

        /// <summary>
        /// Событие для уведомления об изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Создает экземпляр класса <see cref="DataRow"/>.
        /// </summary>
        /// <param name="function">Функция для вычисления результата.</param>
        public DataRow(FunctionBase function)
        {
            _function = function;
        }

        /// <summary>
        /// Возвращает и задает значение X.
        /// </summary>
        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
                    Recalculate();
                }
            }
        }

        /// <summary>
        /// Возвращает и задает значение Y.
        /// </summary>
        public double Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                    Recalculate();
                }
            }
        }

        /// <summary>
        /// Возвращает и задает результат вычисления функции.
        /// </summary>
        public double Result
        {
            get => _result;
            private set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Устанавливает функцию для расчета.
        /// </summary>
        /// <param name="function">Функция для пересчета.</param>
        public void SetFunction(FunctionBase function)
        {
            _function = function;
            Recalculate();
        }

        /// <summary>
        /// Пересчитывает результат функции f(x, y).
        /// </summary>
        private void Recalculate()
        {
            if (_function != null)
            {
                Result = _function.Calculate(X, Y);
            }
        }

        /// <summary>
        /// Уведомляет интерфейс об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
