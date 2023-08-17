using System.Configuration;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace OrderProcessing.Application.Messaging
{
    public class RabbitMQProducer
    {
        private readonly IConfiguration _configuration;
        private readonly string _queueName;

        public RabbitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
            _queueName = _configuration["RabbitMQ:QueueName"].ToString();
        }

        public void PublishOrderPlacedEvent(string orderId)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"],
                VirtualHost = _configuration["RabbitMQ:VirtualHost"]
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var message = $"OrderPlaced: {orderId}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }
}
