using Xunit;

namespace BigNumbers.Tests
{
    public class Debug : TestFixture
    {
        [Fact]
        public void Test()
        {
            int shift = 2;
            int i = 890113664;
            i = 490113664;
            var value = new BigIntBits(i);

            BigInt a = new BigInt(i << shift);
            BigIntBits x = value << shift;

            Assert.True(x == a);
        }
    }
}