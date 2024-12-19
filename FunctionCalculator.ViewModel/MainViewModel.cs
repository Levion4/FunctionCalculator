using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FunctionCalculator.Model;

namespace FunctionCalculator.ViewModel
{
    /// <summary>
    /// Представляет ViewModel для главного окна приложения.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Репозиторий функций, предоставляющий доступ к доступным функциям.
        /// </summary>
        private readonly FunctionRepository _functionRepository;

        /// <summary>
        /// Текущая выбранная функция.
        /// </summary>
        private FunctionBase _selectedFunction;

        /// <summary>
        /// Название текущей выбранной функции.
        /// </summary>
        private string _selectedFunctionName;

        /// <summary>
        /// Коэффициент A для текущей функции.
        /// </summary>
        private double _a;

        /// <summary>
        /// Коэффициент B для текущей функции.
        /// </summary>
        private double _b;

        /// <summary>
        /// Коэффициент C для текущей функции.
        /// </summary>
        private double _c;

        /// <summary>
        /// Значение X, введенное пользователем.
        /// </summary>
        private double _x;

        /// <summary>
        /// Значение Y, введенное пользователем.
        /// </summary>
        private double _y;

        /// <summary>
        /// Событие для уведомления об изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Возвращает список доступных значений для коэффициента C
        /// в зависимости от функции.
        /// </summary>
        public ObservableCollection<double> CoefficientCOptions { get; } = 
            new ObservableCollection<double>();

        /// <summary>
        /// Возвращает список доступных названий функций.
        /// </summary>
        public ObservableCollection<string> FunctionNames { get; }

        /// <summary>
        /// Возвращает данные таблицы.
        /// </summary>
        public ObservableCollection<DataRow> Data { get; set; }

        /// <summary>
        /// Возвращает и задает текущую выбранную функцию.
        /// </summary>
        public string SelectedFunctionName
        {
            get
            {
                return _selectedFunctionName;
            }
            set
            {
                if (_selectedFunctionName != value)
                {
                    _selectedFunctionName = value;
                    OnPropertyChanged();
                    LoadSelectedFunction();
                }
            }
        }

        /// <summary>
        /// Возвращает и задает коэффициент A.
        /// </summary>
        public double A
        {
            get
            {
                return _a;
            }
            set
            {
                if (_a != value)
                {
                    _a = value;
                    OnPropertyChanged();

                    if (_selectedFunction != null)
                    {
                        _selectedFunction.A = _a;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает и задает коэффициент B.
        /// </summary>
        public double B
        {
            get
            {
                return _b;
            }
            set
            {
                if (_b != value)
                {
                    _b = value;
                    OnPropertyChanged();

                    if (_selectedFunction != null)
                    {
                        _selectedFunction.B = _b;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает и задает коэффициент C.
        /// </summary>
        public double C
        {
            get
            {
                return _c;
            }
            set
            {
                if (_c != value)
                {
                    _c = value;
                    OnPropertyChanged();

                    if (_selectedFunction != null)
                    {
                        _selectedFunction.C = _c;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает и задает значение X.
        /// </summary>
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Возвращает и задает значение Y.
        /// </summary>
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Создает экземпляр класса <see cref="MainViewModel"/>.
        /// </summary>
        public MainViewModel()
        {
            _functionRepository = new FunctionRepository();
            FunctionNames = new ObservableCollection<string>(
                _functionRepository.GetFunctionNames());
            Data = new ObservableCollection<DataRow>();
            AddInitialRow();
        }

        /// <summary>
        /// Уведомляет интерфейс об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства.</param>
        private void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));

            if (Data.Count > 0)
            {
                var lastRow = Data[^1];
                if (lastRow.X != 0 && lastRow.Y != 0) 
                {
                    AddRow();
                }
            }
        }

        /// <summary>
        /// Загружает выбранную функцию из
        /// репозитория и обновляет коэффициенты.
        /// </summary>
        private void LoadSelectedFunction()
        {
            _selectedFunction = _functionRepository.GetFunction(SelectedFunctionName);
            CoefficientCOptions.Clear();

            if (_selectedFunction != null)
            {
                A = _selectedFunction.A;
                B = _selectedFunction.B;
                C = _selectedFunction.C;

                var options = SelectedFunctionName switch
                {
                    "Линейная" => new[] { 1.0, 2.0, 3.0, 4.0, 5.0 },
                    "Квадратичная" => new[] { 10.0, 20.0, 30.0, 40.0, 50.0 },
                    "Кубическая" => new[] { 100.0, 200.0, 300.0, 400.0, 500.0 },
                    "4-ой степени" => new[] { 1000.0, 2000.0, 3000.0, 4000.0, 5000.0 },
                    "5-ой степени" => new[] { 10000.0, 20000.0, 30000.0, 40000.0, 50000.0 },
                    _ => Array.Empty<double>()
                };

                foreach (var option in options)
                {
                    CoefficientCOptions.Add(option);
                }

                foreach (var row in Data)
                {
                    row.SetFunction(_selectedFunction);
                }
            }
        }

        /// <summary>
        /// Вычисляет результат функции и добавляет
        /// новую строку в таблицу данных.
        /// </summary>
        private void AddRow()
        {
            if (_selectedFunction != null)
            {
                var newRow = new DataRow(_selectedFunction);
                Data.Add(newRow);
            }
        }

        /// <summary>
        /// Добавляет начальную строку в таблицу.
        /// </summary>
        private void AddInitialRow()
        {
            if (_selectedFunction == null)
            {
                _selectedFunction = _functionRepository.GetFunction(FunctionNames[0]);
                SelectedFunctionName = FunctionNames[0];
            }
            var newRow = new DataRow(_selectedFunction);
            Data.Add(newRow);
        }
    }
}
