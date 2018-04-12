using Xunit;

namespace BigNumbers.Tests
{
    public class BigIntTests : TestFixture
    {
        [Fact]
        public void Should_Implicitly_Convert_Byte()
        {
            byte i = base.GetNextByte();
            BigInt x = new BigInt(i);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_Byte()
        {
            byte i = base.GetNextByte();
            BigInt x = new BigInt(i);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Implicitly_Convert_SignedByte()
        {
            sbyte i = base.GetNextSignedByte();
            BigInt x = new BigInt(i);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_SignedByte()
        {
            sbyte i = base.GetNextSignedByte();
            BigInt x = new BigInt(i);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Implicitly_Convert_Short()
        {
            short i = base.GetNextShort();
            BigInt x = new BigInt(i);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_Short()
        {
            short i = base.GetNextShort();
            BigInt x = new BigInt(i);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Implicitly_Convert_UnsignedShort()
        {
            ushort i = base.GetNextUnsignedShort();
            BigInt x = new BigInt(i);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_UnsignedShort()
        {
            ushort i = base.GetNextUnsignedShort();
            BigInt x = new BigInt(i);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Implicitly_Convert_Int()
        {
            int i = base.GetNextInt();
            BigInt x = new BigInt(i);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_Int()
        {
            int i = base.GetNextInt();
            BigInt x = new BigInt(i);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Implicitly_Convert_UnsignedInt()
        {
            uint i = base.GetNextUnsignedInt();
            BigInt x = new BigInt(i);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_UnsignedInt()
        {
            uint i = base.GetNextUnsignedInt();
            BigInt x = new BigInt(i);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Implicitly_Convert_Long()
        {
            long i = base.GetNextLong();
            BigInt x = new BigInt(i);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_Long()
        {
            long i = base.GetNextLong();
            BigInt x = new BigInt(i);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Implicitly_Convert_UnsignedLong()
        {
            ulong i = base.GetNextUnsignedLong();
            BigInt x = new BigInt(i);

            BigInt a = i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Explicitly_Convert_UnsignedLong()
        {
            ulong i = base.GetNextUnsignedLong();
            BigInt x = new BigInt(i);

            BigInt a = (BigInt)i;

            Assert.Equal(x, a);
        }

        [Fact]
        public void Should_Override_Object_ToString()
        {
            int i = base.GetNextInt();
            BigInt x = i;

            Assert.Equal(i.ToString(), x.ToString());
        }

        [Fact]
        public void Should_Override_Object_Equals()
        {
            byte uv = base.GetNextByte();
            sbyte sv = base.GetNextSignedByte();
            var ux = new BigInt(uv);
            var sx = new BigInt(sv);

            byte ua = uv;
            sbyte sa = sv;
            ushort ub = uv;
            short sb = sv;
            uint uc = uv;
            int sc = sv;
            ulong ud = uv;
            long sd = sv;
            string ue = uv.ToString();
            string se = sv.ToString();
            object f = new object();
            byte[] ug = new [] { uv };
            sbyte[] sg = new [] { sv };

            Assert.True(ux.Equals(ua));
            Assert.True(sx.Equals(sa));
            Assert.True(ux.Equals(ub));
            Assert.True(sx.Equals(sb));
            Assert.True(ux.Equals(uc));
            Assert.True(sx.Equals(sc));
            Assert.True(ux.Equals(ud));
            Assert.True(sx.Equals(sd));
            Assert.False(ux.Equals(ue));
            Assert.False(sx.Equals(se));
            Assert.False(ux.Equals(f));
            Assert.False(sx.Equals(f));
            Assert.False(ux.Equals(ug));
            Assert.False(sx.Equals(sg));
        }

        [Fact]
        public void Should_Override_Object_GetHashCode()
        {
            long i = base.GetNextInt();
            BigInt x = i;

            BigInt a = (i - 1);
            BigInt b = i;
            BigInt c = (i + 1);

            Assert.NotEqual(x.GetHashCode(), a.GetHashCode());
            Assert.Equal(x.GetHashCode(), b.GetHashCode());
            Assert.NotEqual(x.GetHashCode(), c.GetHashCode());
        }

        [Fact]
        public void Should_Implement_CompareTo_BigInt()
        {
            long i = base.GetNextInt();
            BigInt x = i;

            BigInt a = (i - 1);
            BigInt b = i;
            BigInt c = (i + 1);

            Assert.True(x.CompareTo(a) > 0);
            Assert.True(x.CompareTo(b) == 0);
            Assert.True(x.CompareTo(c) < 0);
        }
    }
}