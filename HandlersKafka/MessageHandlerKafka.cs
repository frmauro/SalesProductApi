using Confluent.Kafka;
using System;
using System.Threading;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using SalesProductApi.Models;

namespace SalesProductApi.HandlersKafka
{
    public class MessageHandlerKafka : IHostedService
    {

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "fila-consumer-product",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe("fila_produto");
                var cts = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var message = c.Consume(cts.Token);
                        ItemsDto items = JsonSerializer.Deserialize<ItemsDto>(message.Value);
                        //_logger.LogInformation($"Mensagem: {message.Value} recebida de {message.TopicPartitionOffset}");
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

