using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Receiving;
using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace MQTT_server
{
    class Program
    {
        private static string payload;
        private static MqttServer MqttServer = null;
        static void Main(string[] args)
        {
            _ = MqttAsync(); // ( _= ) ->  press any key to start.
            while (true) ;

        }
        private static async Task MqttAsync()
        {
            var optionsBuilder = new MqttServerOptionsBuilder().WithConnectionBacklog(100).WithDefaultEndpointPort(1883);
            var mqttServer = new MqttFactory().CreateMqttServer();
            await mqttServer.StartAsync(optionsBuilder.Build());
            mqttServer.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(e =>
            {
                Console.WriteLine($"Client:{e.ClientId} Topic:{e.ApplicationMessage.Topic} Message:{Encoding.UTF8.GetString(e.ApplicationMessage.Payload ?? new byte[0])}");
                payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload ?? new byte[0]);
            });
            mqttServer.ClientConnectedHandler = new MqttServerClientConnectedHandlerDelegate(e =>
            {
                Console.WriteLine($"Client:{e.ClientId} 已連接！");
            });
            mqttServer.ClientDisconnectedHandler = new MqttServerClientDisconnectedHandlerDelegate(e =>
            {
                Console.WriteLine($"Client:{e.ClientId}已離線！");
            });
        }
    }
}