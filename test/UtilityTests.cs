using System;
using Xunit;

namespace BigNumbers.Tests
{
    public class OperationsTests : TestFixture
    {
        [Fact]
        public void Should_Correctly_Calculate_Add()
        {
            long a = base.GetNextInt();
            long b = base.GetNextInt();

            var x = Operations.Add(a, b);

            // convert to strings to avoid implicit conversion
            Assert.Equal(string.Format("{0}", a + b), string.Format("{0}", x));
        }

        [Fact]
        public void Should_Correctly_Calculate_Subtract()
        {
            long a = base.GetNextInt();
            long b = base.GetNextInt();

            var x = Operations.Subtract(a, b);

            // convert to strings to avoid implicit conversion
            Assert.Equal(string.Format("{0}", a - b), string.Format("{0}", x));
        }

        [Fact]
        public void Should_Correctly_Calculate_LongMultiplication()
        {
            long a = base.GetNextInt();
            long b = base.GetNextInt();

            var x = Operations.LongMultiplication(a, b);

            // convert to strings to avoid implicit conversion
            Assert.Equal(string.Format("{0}", a * b), string.Format("{0}", x));
        }

        [Fact]
        public void Should_Correctly_Calculate_KaratsubaMultiplication()
        {
            long a = base.GetNextInt();
            long b = base.GetNextInt();

            var x = Operations.KaratsubaMultiplication(a, b);

            // convert to strings to avoid implicit conversion
            Assert.Equal(string.Format("{0}", a * b), string.Format("{0}", x));
        }

        [Fact]
        public void Should_Correctly_Calculate_Pow()
        {
            sbyte i = base.GetNextNumber((sbyte)-9, (sbyte)9);
            byte p = base.GetNextNumber((byte)0, (byte)15);
            BigInt baseValue = i;

            BigInt x = Operations.Pow(baseValue, p);
            var a = (long)Math.Pow(i, p);

            Assert.Equal(a, x);
        }

        [Fact]
        public void Should_Correctly_Calculate_DivRem2()
        {
            int i = base.GetNextInt();
            BigInt x = i;

            sbyte xRem;
            BigInt xQuo = Operations.DivRem2(x, out xRem);
            int aQuo = i / 2, aRem = i % 2;

            Assert.Equal(aQuo, xQuo);
            Assert.Equal(aRem, xRem);
        }

        [Fact]
        public void Should_Correctly_Perform_Right_BitShift_For_Positive_Values()
        {
            byte i = base.GetNextByte();
            BigIntBits x = new BigInt(i);

            x = Operations.BitShift(x, 8, toTheRight: true);

            Assert.True(x == new BigInt(0));
        }

        [Fact]
        public void Should_Correctly_Perform_Right_BitShift_For_Negative_Values()
        {
            sbyte i = base.GetNextNumber(sbyte.MinValue, (sbyte)-1);
            BigIntBits x = new BigInt(i);

            x = Operations.BitShift(x, 8, toTheRight: true);

            Assert.True(x == new BigInt(-1));
        }
    }
}