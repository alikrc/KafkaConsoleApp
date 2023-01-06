using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Net;

namespace KafkaConsoleApp
{
    public class ProducerService : IHostedService
    {
        private readonly string kafkaServerIp = "localhost:9092";
        private readonly string topic = "test-topic";

        public async Task SendMessageAsync(string topic, string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaServerIp,
            };

            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"Delivery Timestamp: {result.Timestamp.UtcDateTime} ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await SendMessageAsync(topic, "hello from dotnet 7!");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}