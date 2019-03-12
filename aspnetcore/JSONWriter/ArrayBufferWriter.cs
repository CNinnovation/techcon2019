using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JSONWriter
{
    public class ArrayBufferWriter : IBufferWriter<byte>, IDisposable
    {
        private byte[] _rentedBuffer;
        private int _written;

        public ArrayBufferWriter(int initialCapacity)
        {
            _rentedBuffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
            _written = 0;
        }

        public void Advance(int count) => _written += count;
        public void Dispose() => throw new NotImplementedException();
        public Memory<byte> GetMemory(int sizeHint = 0) => _rentedBuffer.AsMemory(_written);
        public Span<byte> GetSpan(int sizeHint = 0) => _rentedBuffer.AsSpan(_written);
    }
}
