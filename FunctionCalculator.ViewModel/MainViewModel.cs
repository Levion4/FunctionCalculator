using System.Collections.ObjectModel;
using System.Windows.Input;
using FunctionCalculator.Model;

namespace FunctionCalculator.ViewModel
{
    /// <summary>
    /// Представляет ViewModel для главного окна приложения.
    /// </summary>
    public class MainViewModel : BaseVM
    {
        /// <summary>
        /// Репозиторий функций, предоставляющий доступ к доступным функциям.
        /// </summary>
        private readonly FunctionRepository _functionRepository;

        /// <summary>
        /// Словарь, который хранит список табличных
        /// значений для каждой функции.
        /// </summary>
        private readonly Dictionary
            <string, ObservableCollection<DataRow>> _functionData;

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
        /// Команда для добавления строки в таблицу.
        /// </summary>
        public ICommand AddRowCommand { get; }

        /// <summary>
        /// Команда для удаления последней строки в таблице.
        /// </summary>
        public ICommand RemoveRowCommand { get; }

        /// <summary>
        /// Возвращает данные таблицы, выбранной функции.
        /// </summary>
        public ObservableCollection<DataRow> CurrentData { get; set; }

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

                    if (_selectedFunction != null)
                    {
                        _selectedFunction.A = _a;
                    }

                    UpdateAllRows();
                    OnPropertyChanged();
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

                    if (_selectedFunction != null)
                    {
                        _selectedFunction.B = _b;
                    }

                    UpdateAllRows();
                    OnPropertyChanged();
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

                    if (_selectedFunction != null)
                    {
                        _selectedFunction.C = _c;
                    }

                    UpdateAllRows();
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
            CurrentData = new ObservableCollection<DataRow>();
            AddRowCommand = new RelayCommand(AddRow);
            RemoveRowCommand = new RelayCommand(RemoveRow);

            _functionData = new Dictionary<string, ObservableCollection<DataRow>>();
            foreach (var functionName in FunctionNames)
            {
                _functionData[functionName] = new ObservableCollection<DataRow>
                {
                    new DataRow(_functionRepository.GetFunction(functionName))
                };
            }

            _selectedFunction = _functionRepository.GetFunction(FunctionNames[0]);
            SelectedFunctionName = FunctionNames[0];
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

                CurrentData = _functionData[SelectedFunctionName];

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

                foreach (var row in CurrentData)
                {
                    row.SetFunction(_selectedFunction);
                }

                OnPropertyChanged(nameof(CurrentData));
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
                CurrentData.Add(newRow);
                UpdateAllRows();
            }
        }

        /// <summary>
        /// Удаляет последнюю строку в таблице.
        /// </summary>
        private void RemoveRow()
        {
            if (_selectedFunction != null)
            {
                if (CurrentData.Count > 0)
                {
                    var index = CurrentData.Count - 1;
                    CurrentData.Remove(CurrentData[index]);
                }
            }
        }

        /// <summary>
        /// Обновляет все строки таблицы на основе текущих коэффициентов и функции.
        /// </summary>
        private void UpdateAllRows()
        {
            if (_selectedFunction != null)
            {
                foreach (var row in CurrentData)
                {
                    row.SetFunction(_selectedFunction);
                }
            }
        }
    }
}
