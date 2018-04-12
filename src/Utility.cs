using System;
using System.Linq;
using System.Collections;

namespace BigNumbers
{
    public static class Operations
    {
        public static BigInt LongMultiplication(BigInt value1, BigInt value2)
        {
            var product = new byte[value1.Digits.Length + value2.Digits.Length + 1];
            Sign sign = (value1.IsNegative == value2.IsNegative) ? Sign.Positive : Sign.Negative;

            byte[] digits1 = value1.Digits, digits2 = value2.Digits;
            for (int i = 1; i <= digits1.Length; i++)
            {
                if(digits1[digits1.Length - i] == 0) { continue; }

                for (int j = 1; j <= digits2.Length; j++)
                {
                    var index = (i + j) - 1;
                    var x = (digits1[digits1.Length - i] * digits2[digits2.Length - j]) + product[product.Length - index];
                    int digit = x % 10, carry = x / 10;

                    product[product.Length - (index + 1)] += (byte)carry;
                    product[product.Length - index] = (byte)(digit);
                }
            }

            product = product.SkipWhile(d => d == 0).ToArray(); // truncate any leading zeroes

            return new BigInt(product, sign);
        }

        public static BigInt KaratsubaMultiplication(BigInt value1, BigInt value2)
        {
            Sign sign = (value1.IsNegative == value2.IsNegative) ? Sign.Positive : Sign.Negative;
            byte[] product = DigitHelper.KaratsubaAlgorithm(value1.Digits, value2.Digits);
            return new BigInt(product, sign);
        }

        public static BigInt Add(BigInt value1, BigInt value2)
        {
            if (value1._sign != value2._sign)
            {
                BigInt neg = value1.IsNegative ? value1 : value2;
                BigInt pos = value1.IsNegative ? value2 : value1;
                neg._sign = Sign.Positive; //we are doing an actual subtraction of two positive numbers here
                return Subtract(pos, neg);
            }

            var sign = value1.IsNegative ? Sign.Negative : Sign.Positive;
            byte[] addend1 = value1.Digits, addend2 = value2.Digits;
            byte[] sum = DigitHelper.AddDigits(addend1, addend2);

            return new BigInt(sum, sign);
        }

        public static BigIntBits Add(BigIntBits value1, BigIntBits value2)
        {
            if (value1._sign != value2._sign)
            {
                BigIntBits neg = value1.IsNegative ? value1 : value2;
                BigIntBits pos = value1.IsNegative ? value2 : value1;
                neg._sign = Sign.Positive; //we are doing an actual subtraction of two positive numbers here
                return Subtract(pos, neg);
            }

            var sign = value1.IsNegative ? Sign.Negative : Sign.Positive;
            byte[] addend1 = value1.Bits, addend2 = value2.Bits;
            byte[] sum = DigitHelper.AddDigits(addend1, addend2, baseValue: 2);

            return new BigIntBits(sum, sign);
        }

        public static BigInt Subtract(BigInt value1, BigInt value2)
        {
            value2._sign = value2.IsNegative ? Sign.Positive : Sign.Negative;
            if (value1._sign == value2._sign) { return Add(value1, value2); }

            Sign sign, signValue1 = value1._sign, signValue2 = value2._sign;
            byte[] v1 = value1.Digits, v2 = value2.Digits;

            sign = (DigitHelper.CompareDigits(v1, v2) >= 0) ? signValue1 : signValue2;
            
            byte[] diff = DigitHelper.SubtractDigits(v1, v2);

            return new BigInt(diff, sign);
        }

        public static BigIntBits Subtract(BigIntBits value1, BigIntBits value2)
        {
            value2._sign = value2.IsNegative ? Sign.Positive : Sign.Negative;
            if (value1._sign == value2._sign) { return Add(value1, value2); }

            Sign sign, signValue1 = value1._sign, signValue2 = value2._sign;
            byte[] v1 = value1.Bits, v2 = value2.Bits;

            sign = (DigitHelper.CompareDigits(v1, v2) >= 0) ? signValue1 : signValue2;
            
            byte[] diff = DigitHelper.SubtractDigits(v1, v2, baseValue: 2);

            return new BigInt(diff, sign);
        }
        public static BigInt Divide(BigInt dividend, BigInt divisor)
        {
            return new BigInt("");
        }

        public static BigInt Pow(BigInt baseValue, int power)
        {
            if(power == 0) { return new BigInt(1); }
            if(power == 1) { return baseValue; }
            if(baseValue == 0 || baseValue == 1) { return baseValue; }

            BigInt newValue = baseValue;
            while(power-->1) { newValue *= baseValue; }
            return newValue;
        }

        public static BigInt DivRem2(BigInt value, out sbyte remainder)
        {
            byte[] quotient = new byte[value.Digits.Length];
            remainder = 0;
            for(int i = 0; i < value.Digits.Length; i++)
            {
                quotient[i] += (byte)(remainder * 10);
                quotient[i] += (byte)(value.Digits[i]);
                byte q = (byte)(quotient[i] / 2), r = (byte)(quotient[i] % 2);
                quotient[i] = q; remainder = (sbyte)r;
            }

            quotient = quotient.SkipWhile(x => x == 0).ToArray();
            if(value.IsNegative) { remainder *= -1; } 
            return new BigInt(quotient, value.IsNegative ? Sign.Negative : Sign.Positive);
        }

        public static BigIntBits BitShift(BigIntBits value, int shift, bool toTheRight  = false)
        {
            if(toTheRight) { shift *= -1; }
            if(Math.Abs(shift) >= value.Bits.Length && shift < 0) { return value.IsNegative ? new BigIntBits(new byte[] {1}, Sign.Negative): new BigIntBits(new byte[] {0}); }

            if(!value.IsNegative)
            {
                // truncate if shifting to the right to avoid unnecessary trimming of 0s
                byte[] newValue = DigitHelper.ShiftDigits(value.Bits, shift, truncate: toTheRight);
                return new BigIntBits(newValue, Sign.Positive);
            }

            // we represent BigIntBits as an unsigned value and a sign
            // so, if we are to mimic normal bit-shifting, we need to use two's complement
            //   so we need to convert negative values accordingly before shifting
            //   then restore the bits to the BigIntBits representation after
            var bits = value.Bits.Select(b => Convert.ToBoolean(b)).ToArray();
            var complement = new BitArray(bits.Length * 2, true).Cast<bool>().Select(i => (byte)1).ToArray();
            var inverse = new BitArray(bits).Not().Cast<bool>().Select(t => Convert.ToByte(t)).ToArray();
            Array.Copy(inverse, 0, complement, bits.Length, bits.Length);

            // negative value is inverse of absolute value plus 1
            var trueValue = DigitHelper.AddDigits(complement, new byte[] { 1 }, baseValue: 2);

            var shifted = DigitHelper.ShiftDigits(trueValue, shift, truncate: toTheRight);

            complement = DigitHelper.SubtractDigits(shifted, new byte[] { 1 }, baseValue: 2);
            var temp = complement.SkipWhile(c => c == 1).Select(c => Convert.ToBoolean(c)).ToArray();
            var absValue = new BitArray(temp).Not().Cast<bool>().Select(b => Convert.ToByte(b)).ToArray();

            return new BigIntBits(absValue, Sign.Negative);
        }

        public static int Compare(BigInt value1, BigInt value2)
        {
            if (value1.IsNegative != value2.IsNegative) { return value1.IsNegative ? 1 : -1; }
            
            int result = DigitHelper.CompareDigits(value1.Digits, value2.Digits);
            if(value1.IsNegative) { result *= -1; }
            return result;
        }

        public static int Compare(PackedBigInt value1, PackedBigInt value2)
        {
            if (value1.IsNegative != value2.IsNegative) { return value1.IsNegative ? 1 : -1; }
            
            int result = DigitHelper.CompareDigits(value1.Digits, value2.Digits);
            if(value1.IsNegative) { result *= -1; }
            return result;
        }

        public static int Compare(BigIntBits value1, BigIntBits value2)
        {
            if (value1.IsNegative != value2.IsNegative) { return value1.IsNegative ? 1 : -1; }
            
            int result = DigitHelper.CompareDigits(value1.Bits, value2.Bits);
            if(value1.IsNegative) { result *= -1; }
            return result;
        }
    }
}
