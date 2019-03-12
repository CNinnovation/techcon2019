using System;
using System.Buffers;
using System.Text.Json;

namespace JSONWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteJson();
        }

        private static void WriteJson(IBufferWriter<byte> output, long[] extraData)
        {
            var json = new Utf8JsonWriter(output);
            json.WriteNumber("age", 54);
        }
    }
}
