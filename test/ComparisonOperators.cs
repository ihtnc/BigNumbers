using Xunit;

namespace BigNumbers.Tests
{
    public class ComparisonOperators : TestFixture
    {
        [Fact]
        public void Should_Implement_Operator_Equal()
        {
            long i = base.GetNextInt();
            var x = new BigInt(i);

            var a = new BigInt(i - 1);
            var b = new BigInt(i);
            var c = new BigInt(i + 1);

            Assert.Equal(x, b);

            Assert.False(x == a);
            Assert.True(x == b);
            Assert.False(x == c);
        }

        [Fact]
        public void Should_Implement_Operator_NotEqual()
        {
            long i = base.GetNextInt();
            var x = new BigInt(i);

            var a = new BigInt(i - 1);
            var b = new BigInt(i);
            var c = new BigInt(i + 1);

            Assert.NotEqual(x, a);
            Assert.NotEqual(x, c);

            Assert.True(x != a);
            Assert.False(x != b);
            Assert.True(x != c);
        }

        [Fact]
        public void Should_Implement_Operator_GreaterThan()
        {
            long i = base.GetNextInt();
            var x = new BigInt(i);

            var a = new BigInt(i - 1);
            var b = new BigInt(i);
            var c = new BigInt(i + 1);

            Assert.True(x > a);
            Assert.False(x > b);
            Assert.False(x > c);
        }

        [Fact]
        public void Should_Implement_Operator_GreaterThanOrEqual()
        {
            long i = base.GetNextInt();
            var x = new BigInt(i);

            var a = new BigInt(i - 1);
            var b = new BigInt(i);
            var c = new BigInt(i + 1);

            Assert.True(x >= a);
            Assert.True(x >= b);
            Assert.False(x >= c);
        }

        [Fact]
        public void Should_Implement_Operator_LessThan()
        {
            long i = base.GetNextInt();
            var x = new BigInt(i);

            var a = new BigInt(i - 1);
            var b = new BigInt(i);
            var c = new BigInt(i + 1);

            Assert.False(x < a);
            Assert.False(x < b);
            Assert.True(x < c);
        }

        [Fact]
        public void Should_Implement_Operator_LessThanOrEqual()
        {
            long i = base.GetNextInt();
            var x = new BigInt(i);

            var a = new BigInt(i - 1);
            var b = new BigInt(i);
            var c = new BigInt(i + 1);

            Assert.False(x <= a);
            Assert.True(x <= b);
            Assert.True(x <= c);
        }
    }
}