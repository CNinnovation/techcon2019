using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace ClickToSignalR
{
    public static class IOTEventFunction
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)]HttpRequest req,
            [SignalRConnectionInfo(HubName = "iotHub")]SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("IOTEventFunction")]
        public static Task Run(
            [IoTHubTrigger("messages/events", Connection = "IOTHubConnectionString")]EventData message, 
            [SignalR(HubName = "iotHub")]IAsyncCollector<SignalRMessage> signalRMessages, 
            ILogger log)
        {
            string encoded = Encoding.UTF8.GetString(message.Body.Array);
            log.LogInformation($"IOT Hub trigger processing message {encoded}");
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "buttonClicked",
                    Arguments = new[] { encoded}
                });
        }
    }
}