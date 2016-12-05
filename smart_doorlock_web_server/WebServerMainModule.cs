using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_doorlock_web_server
{
    public class WebServerMainModule : NancyModule
    {
        MainPageSL mainPageSl = MainPageSL.Instance;
        LogSL logSl = LogSL.Instance;
        MqttBroker mqttBroker = MqttBroker.Instance;
        public WebServerMainModule()
        {
            Get["/"] = _ =>
            {
                return View["Views/About.sshtml"];
            };

            Get["/Control"] = _ =>
            {
                return View["Views/Control.sshtml", mainPageSl.GetMainPageData()];
            };

            Get["/Log"] = _ =>
            {
                return View["Views/Log.sshtml", logSl.GetAccessLog()];
            };

            Post["/Subscribe"] = x =>
            {
                string clientId = Request.Form.clientId;
                string topic = Request.Form.topic;
                if (mqttBroker.Subscribe(clientId, topic))
                    return "OK";
                return "Fail";
            };

            Post["/Publish"] = x =>
            {
                string clientId = Request.Form.clientId;
                string topic = Request.Form.topic;
                string message = Request.Form.message;
                mqttBroker.Publish(topic, message);
                
                return "OK";
            };
        }
    }
}
