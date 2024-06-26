﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection= factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("BasicTest", false, false, false, null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Received message {0}...", message);
};
channel.BasicConsume("BasicTest", true, consumer);