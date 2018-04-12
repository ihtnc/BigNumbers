using System;
using System.Collections.Generic;

namespace BigNumbers
{
    public struct BigIntBits : IComparable<BigIntBits>
    {
        internal Sign _sign;
        private byte[] _bits;

        public bool IsNegative { get { return _sign == Sign.Negative; } }
        public byte[] Bits { get { return _bits ?? (_bits = new byte[] {0}); } }

        internal void Initialize(BigInt value)
        {
            BigInt quo = value;
            _sign = quo._sign;
            quo._sign = Sign.Positive;

            var bits = new Stack<byte>();

            while (quo > 0)
            {
                sbyte rem;
                quo = Operations.DivRem2(quo, out rem);
                bits.Push((byte)Math.Abs(rem));
            }

            _bits = bits.ToArray();
        }

        internal BigIntBits(byte[] bits, Sign sign = Sign.Positive) { _bits = bits; _sign = sign; }
        public BigIntBits(BigInt value) : this() { Initialize(value); }
        public BigIntBits(string value) : this() { Initialize(new BigInt(value)); }

        public override string ToString() { return ((BigInt)this).ToString(); }
        public override int GetHashCode() { return ((BigInt)this).GetHashCode(); }

        public override bool Equals(object obj)
        {
            if (obj is BigInt) { return CompareTo((BigInt)obj) == 0; }
            if (obj is BigIntBits) { return CompareTo((BigIntBits)obj) == 0; }

            return false;
        }

        public int CompareTo(BigIntBits other) { return Operations.Compare(this, other); }

        public static BigIntBits operator >>(BigIntBits value, int shifts) { return Operations.BitShift(value, shifts, toTheRight: true); }
        public static BigIntBits operator <<(BigIntBits value, int shifts) { return Operations.BitShift(value, shifts); }

        public static BigIntBits operator +(BigIntBits value1, BigIntBits value2) { return Operations.Add(value1, value2); }
        public static BigIntBits operator -(BigIntBits value1, BigIntBits value2) { return Operations.Subtract(value1, value2); }
        public static BigIntBits operator *(BigIntBits value1, BigIntBits value2) { return Operations.KaratsubaMultiplication(value1, value2); }

        public static implicit operator BigIntBits(BigInt value) { return new BigIntBits(value); }
        public static implicit operator BigInt(BigIntBits value)
        {
            var converted = new BigInt(value.Bits[value.Bits.Length - 1]);
            var baseValue = new BigInt(2);

            for(int i = 2; i <= value.Bits.Length; i++)
            {
                if(value.Bits[value.Bits.Length - i] == 0) continue;
                converted += (Operations.Pow(baseValue, i - 1));
            }

            // do not assign sign if value is zero
            converted._sign = (value.Bits.Length > 1 || value.Bits[0] == 1) ? value._sign : Sign.Positive;

            return converted;
        }
    }
}