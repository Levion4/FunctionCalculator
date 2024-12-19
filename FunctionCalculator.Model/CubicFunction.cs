namespace FunctionCalculator.Model
{
    /// <summary>
    /// Реализация кубической функции: f(x,y) = ax^3 + by^2 + c
    /// </summary>
    public class CubicFunction : FunctionBase
    {
        public override double Calculate(double x, double y)
        {
            return A * Math.Pow(x, 3) + B * Math.Pow(y, 2) + C;
        }
    }
}
