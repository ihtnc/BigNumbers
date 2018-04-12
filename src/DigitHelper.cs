using System;
using System.Linq;

namespace BigNumbers
{
    internal static class DigitHelper
    {
        // 15 = 1111 in binary
        // so doing a bitwise "and" to a value with 00001111 will truncate the last 4 bits of a byte
        private const int LEAST_SIGNIFICANT_DIGIT_MASK = 15;

        public static byte[] Pack(byte[] bigEndianDigits)
        {
            var temp = bigEndianDigits.Reverse().ToArray();
            int r = temp.Length % 2, q = temp.Length / 2;
            var packedDigits = new byte[q + r];

            for (var i = 0; i < temp.Length; i += 2)
            {
                byte secondDigit = 0;
                if (i + 1 < temp.Length) { secondDigit = temp[i + 1]; }

                packedDigits[i / 2] = PackDigit(temp[i], secondDigit);
            }

            return packedDigits.Reverse().ToArray();
        }

        public static byte PackDigit(byte leastSignificantDigit, byte mostSignifcantDigit = 0)
        {
            if (mostSignifcantDigit == 0) { return leastSignificantDigit; }

            var packedDigit = (byte)(mostSignifcantDigit << 4);
            packedDigit += leastSignificantDigit;
            return packedDigit;
        }

        public static byte[] Unpack(byte[] packedDigits)
        {
            var temp = packedDigits.Reverse().ToArray();
            var digits = new byte[temp.Length * 2];

            for (var i = 0; i < temp.Length; i++)
            {
                byte[] converted = UnpackDigit(temp[i]);

                digits[i * 2] = converted[0];

                if (converted.Length == 1) { continue; }

                digits[(i * 2) + 1] = converted[1];
            }

            return digits.Reverse().SkipWhile(d => d == 0).ToArray();
        }

        public static byte[] UnpackDigit(byte packedDigit)
        {
            var leastSignificantDigit = (byte)(packedDigit & LEAST_SIGNIFICANT_DIGIT_MASK);
            var mostSignificantDigit = (byte)(packedDigit >> 4);

            if (mostSignificantDigit == 0) { return new[] { leastSignificantDigit }; }

            return new[] { leastSignificantDigit, mostSignificantDigit };
        }

        public static byte[] KaratsubaAlgorithm(byte[] bigEndianValue1, byte[] bigEndianValue2)
        {
            byte[] value1, value2;
            if(bigEndianValue1.Length > bigEndianValue2.Length)
            {
                value1 = bigEndianValue1;
                value2 = new byte[bigEndianValue1.Length];
                Array.Copy(bigEndianValue2, 0, value2, bigEndianValue1.Length - bigEndianValue2.Length, bigEndianValue2.Length);
            }
            else if (bigEndianValue1.Length < bigEndianValue2.Length)
            {
                value1 = bigEndianValue2;
                value2 = new byte[bigEndianValue2.Length];
                Array.Copy(bigEndianValue1, 0, value2, bigEndianValue2.Length - bigEndianValue1.Length, bigEndianValue1.Length);
            }
            else
            {
                value1 = bigEndianValue1;
                value2 = bigEndianValue2;
            }

            int midPoint = (value1.Length / 2) + (value1.Length % 2);

            byte[] x1 = new byte[value1.Length - midPoint], y1 = new byte[x1.Length];
            Array.Copy(value1, x1, value1.Length - midPoint);
            Array.Copy(value2, y1, value2.Length - midPoint);

            byte[] x2 = new byte[midPoint];
            byte[] y2 = new byte[midPoint];
            Array.Copy(value1, midPoint - (value1.Length % 2), x2, 0, midPoint);
            Array.Copy(value2, midPoint - (value1.Length % 2), y2, 0, midPoint);

            if(value1.Length == 1)
            {
                BigInt p = value1[0] * value2[0];
                return p.Digits;
            }

            int deg = midPoint;
            byte[] a = KaratsubaAlgorithm(x1, y1);
            byte[] c = KaratsubaAlgorithm(x2, y2);
            byte[] b = KaratsubaAlgorithm(AddDigits(x1, x2), AddDigits(y1, y2));
            byte[] ac = AddDigits(a, c);
            b = SubtractDigits(b, ac);
            
            a = ShiftDigits(a, 2 * deg);
            b = ShiftDigits(b, deg);

            byte[] final = AddDigits(a, b);
            final = AddDigits(final, c);
            return final; 
        }

        public static byte[] AddDigits(byte[] bigEndianValue1, byte[] bigEndianValue2, byte baseValue = 10)
        {
            int length = Math.Max(bigEndianValue1.Length, bigEndianValue2.Length);
            var sum = new byte[length + 1]; // we add an extra digit incase the sum carries over

            // we won't iterate over the extra digit
            for (int i = 1; i <= sum.Length - 1; i++)
            {
                int x = sum[sum.Length - i];

                if (i <= bigEndianValue1.Length) { x += bigEndianValue1[bigEndianValue1.Length - i]; }
                if (i <= bigEndianValue2.Length) { x += bigEndianValue2[bigEndianValue2.Length - i]; }

                int digit = x % baseValue, carry = x / baseValue;

                sum[sum.Length - (i + 1)] = (byte)carry;
                sum[sum.Length - i] = (byte)(digit);
            }

            return sum.SkipWhile(d => d == 0).ToArray(); // truncate any leading zeroes 
        }

        public static byte[] SubtractDigits(byte[] bigEndianValue1, byte[] bigEndianValue2, byte baseValue = 10)
        {
            byte[] baseDigits, stepDigits;
            if (CompareDigits(bigEndianValue1, bigEndianValue2) >= 0)
            {
                baseDigits = bigEndianValue1;
                stepDigits = bigEndianValue2;
            }
            else
            {
                baseDigits = bigEndianValue2;
                stepDigits = bigEndianValue1;
            }

            int length = baseDigits.Length;
            var diff = new byte[length];
            var hasBorrowed = false;

            for (int i = 1; i <= diff.Length; i++)
            {
                int x = diff[diff.Length - i];
                int bNum = (i <= baseDigits.Length) ? baseDigits[baseDigits.Length - i] : 0;
                int sNum = (i <= stepDigits.Length) ? stepDigits[stepDigits.Length - i] : 0;

                if (hasBorrowed) { bNum--; } // if we borrow from the previous operation, reduce the value of the current digit
                x += bNum;
                x -= sNum;

                hasBorrowed = bNum < sNum;
                if (hasBorrowed) { x += baseValue; }

                diff[diff.Length - i] = (byte)(x % baseValue);
            }

            diff = diff.SkipWhile(d => d == 0).ToArray(); // truncate any leading zeroes

            // if the last operation "borrowed" from the next digit, "return" the "borrowed" value
            if (hasBorrowed)
            {
                diff[0] = (byte)Math.Abs(diff[0] - baseValue);
            }

            return diff;
        }

        public static int CompareDigits<T>(T[] bigEndianValue1, T[] bigEndianValue2) where T : IComparable
        {
            if (bigEndianValue1.Length > bigEndianValue2.Length) { return 1; }
            if (bigEndianValue1.Length < bigEndianValue2.Length) { return -1; }

            for (var i = 0; i < bigEndianValue1.Length; i++)
            {
                int comparison = bigEndianValue1[i].CompareTo(bigEndianValue2[i]); 
                if (comparison != 0) { return comparison; }
            }

            return 0;
        }

        public static T[] ShiftDigits<T>(T[] bigEndianValue, int shifts, bool truncate = false)
        {
            // negative shift moves values to the right
            // moving to the right makes the value smaller for a big endian value

            if(shifts == 0) { return bigEndianValue; }
            if(truncate && Math.Abs(shifts) >= bigEndianValue.Length) { return new T[]{ default(T) }; }

            if(truncate)
            {
                T[] truncated = new T[bigEndianValue.Length + shifts];
                int truncatedIndex = (shifts < 0) ? 0 : shifts;
                Array.Copy(bigEndianValue, truncatedIndex, truncated, 0, bigEndianValue.Length - Math.Abs(shifts));
                return truncated;
            }

            if(shifts < 0)
            {
                T[] newValue = new T[bigEndianValue.Length];
                shifts = Math.Abs(shifts);
                Array.Copy(bigEndianValue, 0, newValue, shifts, bigEndianValue.Length + shifts);
                return newValue;
            }
            else
            {
                T[] newValue = new T[bigEndianValue.Length + shifts];
                Array.Copy(bigEndianValue, 0, newValue, 0, bigEndianValue.Length);
                return newValue;
            }
        }
    }
}
