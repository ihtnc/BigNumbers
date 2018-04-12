using Xunit;

namespace BigNumbers.Tests
{
    public class AdditionProperties : TestFixture
    {
        [Fact]
        public void Should_Be_Commutative()
        {
            var x = new BigInt(base.GetNextInt());
            var y = new BigInt(base.GetNextInt());

            Assert.Equal(x + y, y + x);
        }

        [Fact]
        public void Should_Be_Associative()
        {
            var x = new BigInt(base.GetNextInt());
            var y = new BigInt(base.GetNextInt());
            var z = new BigInt(base.GetNextInt());

            var xy = (x + y);
            var yz = (y + z);
            var xz = (x + z);

            Assert.Equal(xy + z, x + yz);
            Assert.Equal(xy + z, xz + y);
        }

        [Fact]
        public void Should_Have_Identity()
        {
            var x = new BigInt(base.GetNextInt());
            var y = new BigInt();

            Assert.Equal(x + y, x);
        }
    }
}