using System;
using System.Linq;
using System.Text;

namespace BigNumbers
{
    public struct BigInt : IComparable<BigInt>
    {
        internal Sign _sign;
        internal byte[] _digits;

        public bool IsNegative { get { return _sign == Sign.Negative; } }
        public byte[] Digits { get { return _digits ?? (_digits = new[] {(byte) 0}); } }

        internal void Initialize(string absoluteValue, Sign sign)
        {
            if (absoluteValue == null)
            {
                _digits = new[] { (byte)0 };
                _sign = Sign.Positive;
                return;
            }

            var digits = absoluteValue.Trim().TrimStart('0').ToCharArray().ToArray();
            _digits = new byte[digits.Length];
            _sign = sign;

            for (int i = 0; i < digits.Length; i++)
            {
                if (!char.IsNumber(digits[i])) { throw new InvalidOperationException("Value should be numeric."); }
                _digits[i] = (byte)char.GetNumericValue(digits[i]);
            }
        }

        internal BigInt(byte[] bigEndianDigits, Sign sign = Sign.Positive) { _digits = bigEndianDigits; _sign = sign; }

        internal BigInt(string absoluteValue, Sign sign) : this() { Initialize(absoluteValue, sign); }

        public BigInt(string value = null) : this() 
        {
            bool isNegative = false;
            if (value != null)
            {
                value = value.Trim();
                isNegative = value.StartsWith("-");
                value = value.TrimStart('-');
            }

            Initialize(value, isNegative ? Sign.Negative : Sign.Positive);
        }

        public BigInt(byte value) : this(value.ToString(), Sign.Positive) { }
        public BigInt(ushort value) : this(value.ToString(), Sign.Positive) { }
        public BigInt(uint value) : this(value.ToString(), Sign.Positive) { }
        public BigInt(ulong value) : this(value.ToString(), Sign.Positive) { }

        public BigInt(sbyte value) : this((long)value) { }
        public BigInt(short value) : this((long)value) { }
        public BigInt(int value) : this((long)value) { }
        public BigInt(long value) : this(value == long.MinValue ? long.MaxValue.ToString() : Math.Abs(value).ToString(), value < 0 ? Sign.Negative : Sign.Positive)
        {
            // since |long.MinValue| == |long.MaxValue + 1|, we initialize to long.MaxValue if value == long.MinValue
            // we then have to adjust to the original value after initialization
            if(value == long.MinValue) { _digits[0]++; }
        }

        public override string ToString()
        {
            var value = new StringBuilder(_digits.Length);

            foreach (byte c in Digits) { value.Append(c); }

            return string.Format("{0}{1}", (IsNegative ? "-" : ""), value.ToString().TrimStart('0').PadLeft(1, '0'));
        }

        public override int GetHashCode()
        {
            const int b = 378551;
            int a = 63689;
            int hash = 0;

            // If it overflows then just wrap around
            unchecked
            {
                hash = hash * a + _sign.GetHashCode();
                foreach (byte digit in Digits)
                {
                    hash = hash * a + digit.GetHashCode();
                    a = a * b;
                }
            }

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is sbyte) { return CompareTo((sbyte)obj) == 0; }
            if (obj is byte) { return CompareTo((byte)obj) == 0; }
            if (obj is short) { return CompareTo((short)obj) == 0; }
            if (obj is ushort) { return CompareTo((ushort)obj) == 0; }
            if (obj is int) { return CompareTo((int)obj) == 0; }
            if (obj is uint) { return CompareTo((uint)obj) == 0; }
            if (obj is long) { return CompareTo((long)obj) == 0; }
            if (obj is ulong) { return CompareTo((ulong)obj) == 0; }
            if (obj is BigInt) { return CompareTo((BigInt)obj) == 0; }

            return false;
        }

        public int CompareTo(BigInt other) { return Operations.Compare(this, other); }

        public static BigInt operator +(BigInt value1, BigInt value2) { return Operations.Add(value1, value2); }
        public static BigInt operator -(BigInt value1, BigInt value2) { return Operations.Subtract(value1, value2); }
        public static BigInt operator *(BigInt value1, BigInt value2) { return Operations.KaratsubaMultiplication(value1, value2); }
        public static BigInt operator /(BigInt value1, BigInt value2) { return Operations.Divide(value1, value2); }

        public static bool operator ==(BigInt value1, BigInt value2) { return value1.CompareTo(value2) == 0; }
        public static bool operator !=(BigInt value1, BigInt value2) { return value1.CompareTo(value2) != 0; }
        public static bool operator >(BigInt value1, BigInt value2) { return value1.CompareTo(value2) > 0; }
        public static bool operator >=(BigInt value1, BigInt value2) { return value1.CompareTo(value2) >= 0; }
        public static bool operator <(BigInt value1, BigInt value2) { return value1.CompareTo(value2) < 0; }
        public static bool operator <=(BigInt value1, BigInt value2) { return value1.CompareTo(value2) <= 0; }

        public static implicit operator BigInt(byte value) { return new BigInt(value); }
        public static implicit operator BigInt(sbyte value) { return new BigInt(value); }
        public static implicit operator BigInt(short value) { return new BigInt(value); }
        public static implicit operator BigInt(ushort value) { return new BigInt(value); }
        public static implicit operator BigInt(int value) { return new BigInt(value); }
        public static implicit operator BigInt(uint value) { return new BigInt(value); }
        public static implicit operator BigInt(long value) { return new BigInt(value); }
        public static implicit operator BigInt(ulong value) { return new BigInt(value); }
    }
}