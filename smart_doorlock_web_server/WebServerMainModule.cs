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
        public WebServerMainModule()
        {
            Get["/"] = _ =>
            {
                //MqttBroker.Instance.Publish("/cc3200/ToggleLEDCmdL1", "Test2");
                return View["Views/MainPage.sshtml"];
            };
        }
    }
}
