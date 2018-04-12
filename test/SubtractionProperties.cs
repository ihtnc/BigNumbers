using Xunit;

namespace BigNumbers.Tests
{
    public class SubtractionProperties : TestFixture
    {
        [Fact]
        public void Should_NotBe_Commutative()
        {
            var x = new BigInt(base.GetNextInt());
            var y = new BigInt(base.GetNextInt());

            Assert.NotEqual(x - y, y - x);
        }

        [Fact]
        public void Should_NotBe_Associative()
        {
            var x = new BigInt(base.GetNextInt());
            var y = new BigInt(base.GetNextInt());
            var z = new BigInt(base.GetNextInt());

            var xy = (x - y);
            var yz = (y - z);

            Assert.NotEqual(xy - z, x - yz);
        }

        [Fact]
        public void Should_Have_Identity()
        {
            var x = new BigInt(base.GetNextInt());
            var y = new BigInt();

            Assert.Equal(x - y, x);
        }
    }
}