namespace FunctionCalculator.Model
{
    /// <summary>
    /// Представляет базовый класс функции
    /// с коэффициентами и вычислением значения.
    /// </summary>
    public abstract class FunctionBase
    {
        /// <summary>
        /// Коэффициент A функции.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Коэффициент B функции.
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Коэффициент C функции.
        /// </summary>
        public double C { get; set; }

        /// <summary>
        /// Вычисляет значение функции на основе входных параметров x и y.
        /// </summary>
        /// <param name="x">Значение x.</param>
        /// <param name="y">Значение y.</param>
        /// <returns>Результат вычислеения функции.</returns>
        public abstract double Calculate(double x, double y);
    }
}
