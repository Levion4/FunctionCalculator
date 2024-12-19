namespace FunctionCalculator.Model
{
    /// <summary>
    /// Реализация функции четвертой степени: f(x,y) = ax^4 + by^3 + c
    /// </summary>
    public class FourthDegreeFunction : FunctionBase
    {
        public override double Calculate(double x, double y)
        {
            return A * Math.Pow(x, 4) + B * Math.Pow(y, 3) + C;
        }
    }
}
