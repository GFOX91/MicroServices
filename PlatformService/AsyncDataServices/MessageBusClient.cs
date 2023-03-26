using PlatformService.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient
{
    private const string Exchange = "trigger";
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;


    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"])
        };

        //Connect to RabbitMQ instance
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: Exchange, type: ExchangeType.Fanout);
            //declare event that is triggered when queue shuts down
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("--> Connected to MessageBus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not connect to the Message Bus {ex.Message}");
            throw;
        }
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> Rabbit MQ connection has shutdown...");
    }

    public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
    {
        var message = JsonSerializer.Serialize(platformPublishedDto);

        if (_connection.IsOpen)
        {
            Console.WriteLine("--> Rabbit MQ connection open, sending message...");
            SendMessage(message);
        }
        else
        {
            Console.WriteLine("--> Rabbit MQ connection open, not sending...");
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: Exchange,
                    routingKey: "",
                    basicProperties: null,
                    body: body);

        Console.WriteLine($"We have sent {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("--> Messagebus disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
