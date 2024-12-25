namespace FunctionCalculator.Model
{
    /// <summary>
    /// Реализация квадратичной функции: f(x,y) = ax^2 + by^1 + c
    /// </summary>
    public class QuadraticFunction : FunctionBase
    {
        ///<inheritdoc/>
        public override double Calculate(double x, double y)
        {
            return A * Math.Pow(x, 2) + B * y + C;
        }
    }
}
