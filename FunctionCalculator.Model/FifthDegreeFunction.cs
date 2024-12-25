namespace FunctionCalculator.Model
{
    /// <summary>
    /// Реализация функции пятой степени: f(x,y) = ax^5 + by^4 + c
    /// </summary>
    public class FifthDegreeFunction : FunctionBase
    {
        ///<inheritdoc/>
        public override double Calculate(double x, double y)
        {
            return A * Math.Pow(x, 5) + B * Math.Pow(y, 4) + C;
        }
    }
}
