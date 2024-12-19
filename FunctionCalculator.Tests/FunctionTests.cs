using FunctionCalculator.Model;
using Xunit;

namespace FunctionCalculator.Tests
{
    public class FunctionTests
    {
        [Theory]
        [InlineData(1, 2, 3, 1, 2, 6)]
        public void LinearFunction_Calculate_ReturnsCorrectResult(
            double a, double b, double c, double x, double y, double expected)
        {
            var function = new LinearFunction { A = a, B = b, C = c };
            var result = function.Calculate(x, y);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 3, 1, 2, 8)]
        public void QuadraticFunction_Calculate_ReturnsCorrectResult(
            double a, double b, double c, double x, double y, double expected)
        {
            var function = new QuadraticFunction { A = a, B = b, C = c };
            var result = function.Calculate(x, y);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 3, 1, 2, 12)]
        public void CubicFunction_Calculate_ReturnsCorrectResult(
            double a, double b, double c, double x, double y, double expected)
        {
            var function = new CubicFunction { A = a, B = b, C = c };
            var result = function.Calculate(x, y);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 3, 1, 2, 20)]
        public void FourthDegreeFunction_Calculate_ReturnsCorrectResult(
            double a, double b, double c, double x, double y, double expected)
        {
            var function = new FourthDegreeFunction { A = a, B = b, C = c };
            var result = function.Calculate(x, y);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 3, 1, 2, 36)] 
        public void FifthDegreeFunction_Calculate_ReturnsCorrectResult(
            double a, double b, double c, double x, double y, double expected)
        {
            var function = new FifthDegreeFunction { A = a, B = b, C = c };
            var result = function.Calculate(x, y);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FunctionRepository_GetFunction_ReturnsCorrectFunction()
        {
            var repository = new FunctionRepository();

            var linear = repository.GetFunction("Линейная");
            var quadratic = repository.GetFunction("Квадратичная");

            Assert.NotNull(linear);
            Assert.IsType<LinearFunction>(linear);

            Assert.NotNull(quadratic);
            Assert.IsType<QuadraticFunction>(quadratic);
        }

        [Fact]
        public void FunctionRepository_GetFunctionNames_ReturnsCorrectNames()
        {
            var repository = new FunctionRepository();
            var names = repository.GetFunctionNames();

            Assert.Contains("Линейная", names);
            Assert.Contains("Квадратичная", names);
            Assert.Contains("Кубическая", names);
            Assert.Contains("4-ой степени", names);
            Assert.Contains("5-ой степени", names);
        }
    }
}
