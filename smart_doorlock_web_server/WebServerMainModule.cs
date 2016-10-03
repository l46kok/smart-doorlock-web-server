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
        MqttBroker mqttBroker = MqttBroker.Instance;
        public WebServerMainModule()
        {
            Get["/"] = _ =>
            {
                //MqttBroker.Instance.Publish("/cc3200/ToggleLEDCmdL1", "Test2");
                return View["Views/MainPage.sshtml",mainPageSl.GetMainPageData()];
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
