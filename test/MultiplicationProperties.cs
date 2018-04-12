using Xunit;

namespace BigNumbers.Tests
{
    public class MultiplicationProperties : TestFixture
    {
        [Fact]
        public void Should_Be_Commutative()
        {
            var x = new BigInt(base.GetNextByte());
            var y = new BigInt(base.GetNextByte());

            Assert.Equal(x * y, y * x);
        }

        [Fact]
        public void Should_Be_Associative()
        {
            var x = new BigInt(base.GetNextByte());
            var y = new BigInt(base.GetNextByte());
            var z = new BigInt(base.GetNextByte());

            var xy = (x * y);
            var yz = (y * z);
            var xz = (x * z);

            Assert.Equal(xy * z, x * yz);
            Assert.Equal(xy * z, xz * y);
        }

        [Fact]
        public void Should_Have_Identity()
        {
            var x = new BigInt(base.GetNextByte());
            var y = new BigInt(1);

            Assert.Equal(x * y, x);
        }

        [Fact]
        public void Should_Be_Distributive()
        {
            var x = new BigInt(base.GetNextInt());
            var y = new BigInt(base.GetNextInt());
            var z = new BigInt(base.GetNextInt());

            Assert.Equal((x * (y + z)), ((x * y) + (x * z)));
            Assert.Equal((x * (y - z)), ((x * y) - (x * z)));
        }
    }
}