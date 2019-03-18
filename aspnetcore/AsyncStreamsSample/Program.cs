using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStreamsSample
{
    class Program
    {
        static async Task Main()
        {
            var aDevice = new ADevice();
            await foreach (var x in aDevice.GetSensorData1())
            {
                Console.WriteLine($"{x.Value1} {x.Value2}");
            }

            //var cts = new CancellationTokenSource();
            //cts.CancelAfter(5000);
            //var aDevice = new ADevice();
            //await foreach(var x in aDevice.GetSensorData(cts.Token))
            //{
            //    Console.WriteLine($"{x.Value1} {x.Value2}");
            //}
            Console.WriteLine("finished");
        }
    }
}
