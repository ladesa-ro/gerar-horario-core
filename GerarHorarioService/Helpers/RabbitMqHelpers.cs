using RabbitMQ.Client;

namespace GerarHorarioService.Extensions;

public static class RabbitMqHelpers
{
    

    public static ConnectionFactory RabbitMqConnectionFactory()
    {
        var hostName = Environment.GetEnvironmentVariable("HostName") ?? "localhost";
        var userName = Environment.GetEnvironmentVariable("RabbitMQUserName") ?? "user";
        var password = Environment.GetEnvironmentVariable("Password") ?? "bitnami";

        Console.WriteLine(hostName);
        Console.WriteLine(userName);
        Console.WriteLine(password);


        if (string.IsNullOrEmpty(hostName) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            throw new InvalidOperationException("HostName or UserName is missing.");
        }
        
        return new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password
        };
    }
}