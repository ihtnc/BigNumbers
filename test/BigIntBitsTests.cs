using Xunit;

namespace BigNumbers.Tests
{
    public class BigIntBitsTests : TestFixture
    {
        [Fact]
        public void Should_Implicitly_Convert_BigInt()
        {
            BigInt i = new BigInt(base.GetNextInt());
            BigIntBits x = new BigIntBits(i);

            BigIntBits a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_BigInt()
        {
            BigInt i = new BigInt(base.GetNextInt());
            BigIntBits x = new BigIntBits(i);

            BigIntBits a = (BigIntBits)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Implicitly_Convert_From_BigInt()
        {
            BigInt x = new BigInt(base.GetNextInt());
            BigIntBits i = new BigIntBits(x);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_From_BigInt()
        {
            BigInt x = new BigInt(base.GetNextInt());
            BigIntBits i = new BigIntBits(x);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Have_Left_BitShift_Operation()
        {
            int shift = base.GetNextNumber(0, 5);
            int i = base.GetNextInt();
            var value = new BigIntBits(new BigInt(i));

            BigInt a = new BigInt(i >> shift);
            BigIntBits x = value >> shift;

            Assert.True(x == a);
        }

        [Fact]
        public void Should_Have_Right_BitShift_Operation()
        {
            int shift = base.GetNextNumber(0, 5);
            int i = base.GetNextShort();
            var value = new BigIntBits(new BigInt(i));

            BigInt a = new BigInt(i << shift);
            BigIntBits x = value << shift;

            Assert.True(x == a);
        }

        [Fact]
        public void Should_Override_Object_ToString()
        {
            int i = base.GetNextInt();
            BigIntBits x = new BigInt(i);

            Assert.Equal(i.ToString(), x.ToString());
        }

        [Fact]
        public void Should_Override_Object_Equals()
        {
            BigInt a = base.GetNextInt();
            BigIntBits b = a;

            BigIntBits x = a;

            Assert.True(x.Equals(a));
            Assert.True(x.Equals(b));
        }

        [Fact]
        public void Should_Override_Object_GetHashCode()
        {
            long i = base.GetNextInt();
            BigIntBits x = new BigInt(i);

            BigIntBits a = new BigInt(i - 1);
            BigIntBits b = new BigInt(i);
            BigIntBits c = new BigInt(i + 1);

            Assert.NotEqual(x.GetHashCode(), a.GetHashCode());
            Assert.Equal(x.GetHashCode(), b.GetHashCode());
            Assert.NotEqual(x.GetHashCode(), c.GetHashCode());
        }

        [Fact]
        public void Should_Implement_CompareTo_BigIntBits()
        {
            long i = base.GetNextInt();
            BigIntBits x = new BigInt(i);

            BigIntBits a = new BigInt(i - 1);
            BigIntBits b = new BigInt(i);
            BigIntBits c = new BigInt(i + 1);

            Assert.True(x.CompareTo(a) > 0);
            Assert.True(x.CompareTo(b) == 0);
            Assert.True(x.CompareTo(c) < 0);
        }
    }
}