using System.Text;
using RabbitMQ.Client;

namespace ProfilesApi.RabbitMq;

/*public class RabbitMWProducer : IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "MyQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
           // var json = JsonConvert.SerializeObject(message);
         //   var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                routingKey: "MyQueue",
                basicProperties: null,
                body: body);
        }
    }
    */
//}
