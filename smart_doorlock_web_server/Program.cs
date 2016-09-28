using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Communication;

namespace smart_doorlock_web_server
{
    class Program
    {

        static void Main(string[] args)
        {

#if TRACE
            //MqttUtility.Trace.TraceLevel = MqttUtility.TraceLevel.Verbose | MqttUtility.TraceLevel.Frame;
            //MqttUtility.Trace.TraceListener = (f, a) => System.Diagnostics.Trace.WriteLine(System.String.Format(f, a));
#endif
            MqttSettings settings = MqttSettings.Instance;
            MqttTcpCommunicationLayer cLayer = new MqttTcpCommunicationLayer(settings.Port);
            // create and start broker
            MqttBroker broker = new MqttBroker(cLayer, settings);

            cLayer.ClientConnected += cLayer_ClientConnected;
                        
            broker.Start();
            Console.ReadLine();
            broker.Stop();
        }

        static void cLayer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("Client Connected");
        }
    }
}
