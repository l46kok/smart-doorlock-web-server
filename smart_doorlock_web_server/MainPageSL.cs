using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace smart_doorlock_web_server
{
    public class MainPageSL
    {
        MqttBroker mqttBroker = MqttBroker.Instance;
        private static readonly MainPageSL _instance = new MainPageSL();
        public static MainPageSL Instance
        {
            get
            {
                return _instance;
            }
        }

        private MainPageSL() { }
        public List<MainPageData> GetMainPageData()
        {
            List<MainPageData> dataList = new List<MainPageData>();
            MqttClientCollection clientList = mqttBroker.GetClientList();
            int doorlockNo = 1;
            foreach (MqttClient client in clientList) {
                MainPageData data = new MainPageData();
                data.DoorlockNo = doorlockNo;
                data.ClientId = client.ClientId;
                data.ProtocolVer = client.ProtocolVersion.ToString();
                dataList.Add(data);
                doorlockNo++;
            }

            return dataList;
        }
    }
}
