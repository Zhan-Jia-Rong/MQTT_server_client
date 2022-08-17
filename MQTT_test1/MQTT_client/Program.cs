using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.IO;


using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;


using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Client.Receiving;
using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Threading.Tasks;

namespace MQTT_client
{
    class Program
    {
        private static string payloadText;
        static void Main(string[] args)
        {
            MqttClientAsync();
            while (true) ;
        }
        private static async Task MqttClientAsync()
        {
            var factory = new MqttFactory();

            var clientOptions = new MqttClientOptionsBuilder()
                .WithClientId("TestClient")
                .WithTcpServer("140.112.45.232", 1883)
                .Build();

            var client = factory.CreateMqttClient();
            client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(msg =>
            {
                payloadText = Encoding.UTF8.GetString(msg?.ApplicationMessage?.Payload ?? Array.Empty<byte>());

                Console.WriteLine($"Received msg: {payloadText}");
            });
            /*
            client.UseApplicationMessageReceivedHandler(msg =>
            {
                var payloadText = Encoding.UTF8.GetString(
                    msg?.ApplicationMessage?.Payload ?? Array.Empty<byte>());

                Console.WriteLine($"Received msg: {payloadText}");
            });
            */
            await client.ConnectAsync(clientOptions, CancellationToken.None);

            await client.SubscribeAsync(
                new MqttTopicFilter
                {
                    //Topic = "ITRI1_Result"
                    //Topic = "ITRI1_MonitorReport"
                    Topic = "ITRI1_SensorMessage"
                }
            ); ;
        }
    }
}