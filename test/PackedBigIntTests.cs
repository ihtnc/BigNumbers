using Xunit;

namespace BigNumbers.Tests
{
    public class PackedBigIntTests : TestFixture
    {
        [Fact]
        public void Should_Implicitly_Convert_BigInt()
        {
            BigInt i = new BigInt(base.GetNextInt());
            PackedBigInt x = new PackedBigInt(i);

            PackedBigInt a = i;

            Assert.Equal(x.ToString(), a.ToString());
        }

        [Fact]
        public void Should_Explicitly_Convert_BigInt()
        {
            BigInt i = new BigInt(base.GetNextInt());
            PackedBigInt x = new PackedBigInt(i);

            PackedBigInt a = (PackedBigInt)i;

            Assert.Equal(x.ToString(), a.ToString());
        }

        [Fact]
        public void Should_Implicitly_Convert_From_BigInt()
        {
            BigInt x = new BigInt(base.GetNextInt());
            PackedBigInt i = new PackedBigInt(x);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_From_BigInt()
        {
            BigInt x = new BigInt(base.GetNextInt());
            PackedBigInt i = new PackedBigInt(x);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Have_Compressed_Digits()
        {
            int i = base.GetNextInt();
            BigInt x = i;

            PackedBigInt a = new BigInt(i);
            int packedLength = x.Digits.Length / 2;

            Assert.True(a.Digits.Length == packedLength || a.Digits.Length == packedLength + 1);
        }

        [Fact]
        public void Should_Override_Object_ToString()
        {
            int i = base.GetNextInt();
            PackedBigInt x = new BigInt(i);

            Assert.Equal(i.ToString(), x.ToString());
        }

        [Fact]
        public void Should_Override_Object_Equals()
        {
            BigInt a = base.GetNextInt();
            PackedBigInt b = a;

            PackedBigInt x = a;

            Assert.True(x.Equals(a));
            Assert.True(x.Equals(b));
        }

        [Fact]
        public void Should_Override_Object_GetHashCode()
        {
            long i = base.GetNextInt();
            PackedBigInt x = new BigInt(i);

            PackedBigInt a = new BigInt(i - 1);
            PackedBigInt b = new BigInt(i);
            PackedBigInt c = new BigInt(i + 1);

            Assert.NotEqual(x.GetHashCode(), a.GetHashCode());
            Assert.Equal(x.GetHashCode(), b.GetHashCode());
            Assert.NotEqual(x.GetHashCode(), c.GetHashCode());
        }

        [Fact]
        public void Should_Implement_CompareTo_PackedBigInt()
        {
            long i = base.GetNextInt();
            PackedBigInt x = new BigInt(i);

            PackedBigInt a = new BigInt(i - 1);
            PackedBigInt b = new BigInt(i);
            PackedBigInt c = new BigInt(i + 1);

            Assert.True(x.CompareTo(a) > 0);
            Assert.True(x.CompareTo(b) == 0);
            Assert.True(x.CompareTo(c) < 0);
        }
    }
}