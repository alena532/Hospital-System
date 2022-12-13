namespace ProfilesApi.RabbitMq;

public interface IMessageProducer
{
    void SendMessage<T> (T message);
}