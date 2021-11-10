using Assistant.Application.Services;
using FluentAssertions;
using Xunit;

namespace Assistant.UnitTests
{
    // The AAA pattern for unit tests: https://automationpanda.com/2020/07/07/arrange-act-assert-a-pattern-for-writing-good-tests/
    public class UnitTest1
    {
        // Setup should 
        public UnitTest1()
        { 

        }

        [Theory] // TestsWithInlinedParameters, thus we don't need to know what is the value we are expecting
        [InlineData(5, 4)]
        [InlineData(10, 1000)]
        public void Test_ShouldAddTwoIntegerNumbers(int left, int right)
        {
            // Arrange
            var unitTestable = new UnitTestable();

            // Act
            var result = unitTestable.Add(left, right);

            // Assert
            result.Should().Be(left + right);
        }

        [Fact(DisplayName = "Multiply two integer numbers")] // Tests without inline parameters
        public void Test_ShouldMultiplyTwoIntegerNumbers()
        {
            // Arrange
            var left = 5;
            var right = 10;
            var unitTestable = new UnitTestable();


            // Act
            var result = unitTestable.Multiply(left, right);

            // Assert
            result.Should().Be(50);
        }
    }
}
