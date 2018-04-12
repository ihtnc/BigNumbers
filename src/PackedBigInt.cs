using System;

namespace BigNumbers
{
    public struct PackedBigInt  : IComparable<PackedBigInt>
    {
        internal Sign _sign;
        private byte[] _digits;

        public bool IsNegative { get { return _sign == Sign.Negative; } }
        public byte[] Digits { get { return _digits ?? (_digits = new[] { (byte)0 }); } }

        internal PackedBigInt(byte[] packedDigits, Sign sign = Sign.Positive) { _digits = packedDigits; _sign = sign; }

        public PackedBigInt(BigInt value) : this(DigitHelper.Pack(value.Digits), value._sign) { }
        public PackedBigInt(string value)
        {
            var b = new BigInt(value);
            _digits = DigitHelper.Pack(b.Digits);
            _sign = b._sign;
        }

        public override string ToString() { return ((BigInt)this).ToString(); }
        public override int GetHashCode() { return ((BigInt)this).GetHashCode(); }

        public override bool Equals(object obj)
        {
            if (obj is BigInt) { return CompareTo((BigInt)obj) == 0; }
            if (obj is PackedBigInt) { return CompareTo((PackedBigInt)obj) == 0; }

            return false;
        }

        public int CompareTo(PackedBigInt other) { return Operations.Compare(this, other); }

        public static implicit operator BigInt(PackedBigInt value) { return new BigInt(DigitHelper.Unpack(value.Digits), value._sign); }
        public static implicit operator PackedBigInt(BigInt value) { return new PackedBigInt(DigitHelper.Pack(value.Digits), value._sign); }
    }
}