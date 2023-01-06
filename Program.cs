using KafkaConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddHostedService<ProducerService>();
    services.AddHostedService<ConsumerService>();
});

builder.Build().Run();