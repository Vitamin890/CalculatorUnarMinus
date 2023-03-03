namespace ConsoleApp.Tests
{
    public class CalculatorTest
    {
        [Fact]
        public void TestMethod_UnarMinus()
        {
            // -2+13-(2*2)/3

            //arrange
            string operators = "-2+13-(2*2)/3";
            string expected = "0-2+13-(2*2)/3";

            //act
            string actual = Calculator.UnarMinus(operators);

            //assert
            Assert.Equal(expected, actual);
        }
    }
}