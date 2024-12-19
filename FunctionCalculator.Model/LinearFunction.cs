namespace FunctionCalculator.Model
{
    /// <summary>
    /// Реализация линейной функции: f(x,y) = ax + by^0 + c
    /// </summary>
    public class LinearFunction : FunctionBase
    {
        public override double Calculate(double x, double y)
        {
            return A * x + B * 1 + C;
        }
    }
}
