using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStreamsSample
{
    public class ADevice
    {
        public async IAsyncEnumerable<SensorData> GetSensorData(CancellationToken cancellationToken = default)
        {
            bool cancel = false;
            using (cancellationToken.Register(() => cancel = true))
            {
                var r = new Random();
                while (!cancel)
                {
                    await Task.Delay(r.Next(500));
                    yield return new SensorData()
                    {
                        Value1 = r.Next(100),
                        Value2 = r.Next(100)
                    };
                }
                Console.WriteLine("cancel requested");
            };
        }

        public async IAsyncEnumerable<SensorData> GetSensorData1()
        {
            var r = new Random();
            while (true)
            {
                await Task.Delay(r.Next(300));
                yield return new SensorData()
                {
                    Value1 = r.Next(100),
                    Value2 = r.Next(100)
                };
            }
        }
    }
}
