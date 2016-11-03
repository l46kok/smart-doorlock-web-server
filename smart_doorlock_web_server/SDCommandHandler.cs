using smart_doorlock_web_server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_doorlock_web_server
{
    public static class SDCommandHandler
    {
        public static void HandleCommand(string payload)
        {
            string[] splitPayload = payload.Split('|');
            if (splitPayload.Length < 2)
                return;
            string cmd = splitPayload[0];
            if (cmd.Equals("LOG",StringComparison.InvariantCultureIgnoreCase))
            {
                string type = splitPayload[1];
                string data = splitPayload[2];
                SDAccessLogDTO accessLog = new SDAccessLogDTO();
                accessLog.AccessDateTime = DateTime.Now;
                accessLog.Data = data;
                accessLog.Type = type;
                SDAccessLogDAL.InsertAccess(accessLog);
            }
        }
    }
}
