using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SampleFunctions
{
    public class SampleFunction1
    {
        private readonly ILogger<SampleFunction1> _logger;

        public SampleFunction1(ILogger<SampleFunction1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(SampleFunction1))]
        public async Task Run(
            [ServiceBusTrigger("sample-function-1", "sample-function-1-subscription", Connection = "ConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

             // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
