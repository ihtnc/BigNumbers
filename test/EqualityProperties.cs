using Xunit;

namespace BigNumbers.Tests
{
    public class EqualityProperties : TestFixture
    {
        [Fact]
        public void Should_Be_Reflexive()
        {
            var x = new BigInt(base.GetNextInt());

            Assert.Equal(x, x);

#pragma warning disable CS1718
            //required since we have overridden the == operator
            Assert.True(x == x);
#pragma warning restore CS1718
        }

        [Fact]
        public void Should_Be_Symmetric()
        {
            int a = base.GetNextInt();
            var x = new BigInt(a);
            var y = new BigInt(a);

            Assert.Equal(x, y);
            Assert.Equal(y, x);

            Assert.True(x == y);
            Assert.True(y == x);
        }

        [Fact]
        public void Should_Be_Transitive()
        {
            int a = base.GetNextInt();
            var x = new BigInt(a);
            var y = new BigInt(a);
            var z = new BigInt(a);

            Assert.Equal(x, y);
            Assert.Equal(y, z);
            Assert.Equal(x, z);

            Assert.True(x == y);
            Assert.True(y == z);
            Assert.True(x == z);
        }

        [Fact]
        public void Should_ApplyTo_Addition()
        {
            int a = base.GetNextInt();
            int b = base.GetNextInt();
            var x = new BigInt(a);
            var y = new BigInt(a);
            var z = new BigInt(b);

            Assert.Equal(x + z, y + z);
        }

        [Fact]
        public void Should_ApplyTo_Subtraction()
        {
            int a = base.GetNextInt();
            int b = base.GetNextInt();
            var x = new BigInt(a);
            var y = new BigInt(a);
            var z = new BigInt(b);

            Assert.Equal(x - z, y - z);
        }

        [Fact]
        public void Should_ApplyTo_Multiplication()
        {
            int a = base.GetNextInt();
            int b = base.GetNextInt();
            var x = new BigInt(a);
            var y = new BigInt(a);
            var z = new BigInt(b);

            Assert.Equal(x * z, y * z);
        }
    }
}