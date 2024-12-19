namespace FunctionCalculator.Model
{
    /// <summary>
    /// Хранилище коэффициентов и функций.
    /// </summary>
    public class FunctionRepository
    {
        private readonly Dictionary<string, FunctionBase> _functions;

        /// <summary>
        /// Создает экземпляр класса <see cref="FunctionRepository"/>.
        /// </summary>
        public FunctionRepository()
        {
            _functions = new Dictionary<string, FunctionBase>
            {
                { "Линейная", new LinearFunction() },
                { "Квадратичная", new QuadraticFunction() },
                { "Кубическая", new CubicFunction() },
                { "4-ой степени", new FourthDegreeFunction() },
                { "5-ой степени", new FifthDegreeFunction() }
            };
        }

        /// <summary>
        /// Получает фукнцию по ее названию.
        /// </summary>
        /// <param name="name">Название функции.</param>
        /// <returns>
        /// Экземпляр класса, представляющий соответствующую функцию,
        /// или null, если функция с указанным названием не найдена.
        /// </returns>
        public FunctionBase GetFunction(string name)
        {
            return _functions.TryGetValue(
                name, out var function) ? function : null;
        }

        /// <summary>
        /// Получает список всех доступных функций.
        /// </summary>
        /// <returns>
        /// Коллекция строк, каждая из которых
        /// представляет название доступной функции.
        /// </returns>
        public IEnumerable<string> GetFunctionNames()
        {
            return _functions.Keys;
        }
    }
}
