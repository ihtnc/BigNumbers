using System;

namespace BigNumbers.Tests
{
    public abstract class TestFixture : IDisposable
    {
        private readonly Random _rnd;
        public TestFixture() { _rnd = new Random(Guid.NewGuid().GetHashCode()); }
        public void Dispose() { }

        public int GetNextInt() { return Random(int.MinValue, int.MaxValue); }
        public uint GetNextUnsignedInt() { return (uint)Random(0, int.MaxValue); }
        public byte GetNextByte() { return Random(byte.MinValue, byte.MaxValue); }
        public sbyte GetNextSignedByte() { return Random(sbyte.MinValue, sbyte.MaxValue); }
        public short GetNextShort() { return Random(short.MinValue, short.MaxValue); }
        public ushort GetNextUnsignedShort() { return Random(ushort.MinValue, ushort.MaxValue); }
        public long GetNextLong() { return (long)Random(int.MinValue, int.MaxValue); }
        public ulong GetNextUnsignedLong() { return (ulong)Random(0, int.MaxValue); }

        public T GetNextNumber<T>(T minValue, T maxValue) where T : IConvertible { return Random(minValue, maxValue); }

        public T Random<T>(T minValue, T maxValue) where T : IConvertible
        {
            unchecked 
            {
                T value = default(T);
                value = (T)Convert.ChangeType(_rnd.Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue)), typeof(T));
                return value;
            }
        }
    }
}
