using Nancy.Hosting.Self;
using smart_doorlock_web_server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_doorlock_web_server
{
    class Program
    {
        private const string WEB_SERVER_PORT = "8203";
        private const string TERMINATE_STRING = "/Terminate";
        private const string DEFAULT_CONFIG_FILE_PATH = "config.cfg";
        static void Main(string[] args)
        {

            ConfigHandler configHandler = new ConfigHandler(DEFAULT_CONFIG_FILE_PATH);
            if (!configHandler.IsConfigFileValid())
                Console.WriteLine("Error loading default config file: {0}", DEFAULT_CONFIG_FILE_PATH);
            else
            {
                Console.WriteLine("Loading default config file: {0}", DEFAULT_CONFIG_FILE_PATH);
                configHandler.LoadConfig();
                SDDatabase.Instance.LoadConnectionInfo(configHandler.DatabaseServer,
                                                        configHandler.DatabasePort,
                                                        configHandler.DatabaseUserName,
                                                        configHandler.DatabasePassword,
                                                        configHandler.DatabaseName);
            }

            MqttBroker mqttBroker = MqttBroker.Instance;
#if TRACE
            //MqttUtility.Trace.TraceLevel = MqttUtility.TraceLevel.Verbose | MqttUtility.TraceLevel.Frame;
            //MqttUtility.Trace.TraceListener = (f, a) => System.Diagnostics.Trace.WriteLine(System.String.Format(f, a));
#endif
            mqttBroker.Start();
            
            var uri = new Uri("http://localhost:" + WEB_SERVER_PORT + "/");
            var config = new HostConfiguration
            {
                UrlReservations = { CreateAutomatically = true },
                AllowChunkedEncoding = false
            };

            var host = new NancyHost(config, uri);

            try
            {
                host.Start();

                Console.Write("Smart Doorlock Web Server\n" +
                    "\t\"" + uri + "\"\n" +
                    "To quit, input \"" + TERMINATE_STRING + "\".\n\n");
                do Console.Write("> "); while (Console.ReadLine() != TERMINATE_STRING);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unhandled exception has been occured!\n"
                    + e.Message);
                Console.ReadKey(true);
            }
            finally
            {
                host.Stop();
                mqttBroker.Stop();
            }
        }
    }
}
