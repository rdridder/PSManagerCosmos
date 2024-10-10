using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ProcessFunctions
{
    public class TaskResponseFunction
    {
        private readonly ILogger<TaskResponseFunction> _logger;

        public TaskResponseFunction(ILogger<TaskResponseFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(TaskResponseFunction))]
        public async Task Run(
            [ServiceBusTrigger("process-task-finished", "process-task-finished-subscription-core", Connection = "ASB")]
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
